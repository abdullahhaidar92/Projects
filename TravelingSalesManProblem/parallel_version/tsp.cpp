/*
* Parallel version for traveller salesman problem using genetic algorithm
* Solved by Abdullah Haidar 
*----------------------------------------------------------------------------------------	
* Genetic Operations used:
* ==> Selection : Roulette
* ==> Crossover : Cyclic  or  Multippoint
* ==> Mutation : Swapping or Scramble
*----------------------------------------------------------------------------------------	
* MPI :
* Every certain period all nodes exchange most fit chromosomes.
* The master gathers all the chromosomes at the end and obtains the best chromosome.
* Population size is relevant to the speed and capacity of the node.
* ----------------------------------------------------------------------------------------	
* OMP used in :
* >>tsp.cpp in functions:
*  fromChromosomesToBuffer()
*  fromBufferToChromosomes()
* >>tsp.h in functions:
*  Matrix:: Matrix()
*  Chromosome:: calculateDistance,shuffle(), compare()
*  Generation:: Generation(),getAverageDistance(),nextPopulation(),hasDuplicate(),
*             	addMigratedPopulation(),cyclic_crossover(),mostFit(),rouletteSelection(),
*----------------------------------------------------------------------------------------			
*/

// WE MUST SET THE OMP_NUM_THREADS global variable to have parallel threads 
// command_line >> export OMP_NUM_THREADS=4


#include "tsp.h"
#include <iostream>
#include <limits>
#include <assert.h>
#include "mpi.h"

using namespace std;
enum genetic_operator { MULTIPOINT_CROSSOVER,
			            CYCLE_CROSSOVER,
			            SWAP_MUTATION,
			            SCRAMBLE_MUTATION
			          };
char cities_file[255]="";
City *cities;
Node* nodes;
char options_names[][255]={"Change Mutation Operator",
				        "Change Crossover Operator",
				        "Change Mutation Rate",
				        "Change Scramble Rate",
				        "Change Crossover rate"
					   };
bool options[5]={0,0,0,0,0};
int myRank=0,receiveFrom,sendTo;
int *sendBuffer,migration_size;
int *receiveBuffer;
genetic_operator crossover,mutation;

int read_parameters_from_file();
int read_cities_from_file();
char* getProcessorName();
int getNodeIndex(char*);
void setSubPopulationsSizes();
void printSubPopulationsSizes();
void ChangeParameters(Generation*,int*);
int *fromChromosomesToBuffer(Chromosome*,int);
Chromosome* fromBufferToChromosomes(int*, int);

int main(int argc, char *argv[])
{
	int p=0;
	Chromosome *best_chromosome,*local_best;
	
	read_parameters_from_file();
    int nb_cities = read_cities_from_file();
    distances = Matrix(cities, nb_cities);
	setSubPopulationsSizes();
	
	
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &myRank);
	MPI_Comm_size(MPI_COMM_WORLD, &p);
    
	
	
	if(p!=NODES_SIZE)
	{
		if(myRank==0)
		{
			 printf("Number of processes is not equal to the number of nodes in configuration file \n");
		     printf("Number of processes is: %d \n",p);
		     printf("Number of nodes in generation.config is: %d \n",NODES_SIZE);
			//MPI_Abort(MPI_COMM_WORLD,0);
		}
		MPI_Finalize();
		return 0;
		
	}
	
	
	
		
	
	if(myRank==p-1)
		sendTo=0;
	else
		sendTo=myRank+1;
	if(myRank==0)
     	receiveFrom=p-1;
	else
		receiveFrom=myRank-1;
	
	
	
	Generation *generation = new Generation(myRank);
	generation->newPopulationCount=0;
    generation->useSelectionOperation(rouletteSelection);
     //generation->useCrossoverOperation(multipoint_crossover);
     generation->useCrossoverOperation(cyclic_crossover);
	crossover=CYCLE_CROSSOVER;
     //generation->useMutationOperation(swapping_mutation);
     generation->useMutationOperation(scramble_mutate);
	mutation=SCRAMBLE_MUTATION;
	
    clock_t start=clock();
	float time_passed=0;
	clock_t next_time_to_send=0;
	int generations = 0;
	int generations_with_no_improvements = 0;
	double bestFitness = -1;
	double initialAverage = generation->getAverageDistance();
    MPI_Request send_request,recv_request;
	next_time_to_send=start+EXCHANGE_PERIOD_SECONDS*CLOCKS_PER_SEC;
	
	
     while(time_passed < EXECUTION_TIME_SECONDS)
       { 			 
		generation->nextPopulation();
		generations++;
		double newFitness = generation->getBestFitness();
		if(newFitness > bestFitness)
		{
			bestFitness = newFitness;
			generations_with_no_improvements = 0;
			printf("Process %d : Best fitness until now: %f\n",myRank,bestFitness);
			 
		}
		else
		{    
			generations_with_no_improvements++;
		}
         //system("clear");
		 
         // If you are stuck then explore  the available options
		 // Change the parameters so any stuck process could continue
	     ChangeParameters(generation,&generations_with_no_improvements);
		 
		 
		  generation->newPopulationCount=0;
		 
		 if(clock() >= next_time_to_send)
		 {
			 printf("Process %d : Sending to process %d  %d chromosomes\n",myRank,sendTo, migration_size);
			 sendBuffer=fromChromosomesToBuffer(generation->mostFit(migration_size),migration_size);
			 MPI_Isend(sendBuffer, migration_size*CHROMOSOME_LENGTH, MPI_INT, sendTo, 0, MPI_COMM_WORLD,&send_request);
			 printf("Process %d : Chromosomes sent to %d \n",myRank,sendTo);
			 printf("Process %d : Receiving from  %d  %d chromosomes \n",myRank,sendTo,migration_size);
			 receiveBuffer=new int[migration_size*CHROMOSOME_LENGTH];
			 MPI_Irecv(receiveBuffer, migration_size*CHROMOSOME_LENGTH, MPI_INT, receiveFrom, 0, MPI_COMM_WORLD,&recv_request);
			 printf("Process %d : Chromosomes received %d \n",myRank,sendTo);
			 MPI_Wait(&recv_request,MPI_STATUS_IGNORE);
			 generation->migratedPopulation=fromBufferToChromosomes(receiveBuffer, migration_size);
			 generation->addMigratedPopulation(migration_size);
			 next_time_to_send+=EXCHANGE_PERIOD_SECONDS*CLOCKS_PER_SEC;
			 MPI_Barrier(MPI_COMM_WORLD);
		 
		 }
		time_passed = (float)(clock() - start) / CLOCKS_PER_SEC;
         
	}
       
	
 
	printf("<===  PROCESS %d has finished  ===> \n",myRank); 
	printf("Process %d : Number of generations: %d \n",myRank,generations );
	printf("Process %d : Best chromosone info:\n",myRank);
	printf("Process %d : ",myRank);
	generation->bestChromosone->print();
	printf("Process %d : Best fitness: %f \n" ,myRank,generation->getBestFitness());
	printf("Process %d : Distance: %f \n",myRank,generation->getLowestTotalDistance() );
	printf("Process %d : Average distance: %f \n" ,myRank,generation->getAverageDistance());
	printf("Process %d : Initial average: %f \n", myRank,initialAverage );
	
	
	
	local_best=generation->bestChromosone;
	double best_fitness,local_fitness=1/local_best->distance;
	receiveBuffer=new int[NODES_SIZE*CHROMOSOME_LENGTH];
	sendBuffer=fromChromosomesToBuffer(local_best,1);
	
	
	MPI_Reduce(&local_fitness,&best_fitness,1, MPI_DOUBLE, MPI_MAX, MASTER, MPI_COMM_WORLD);
	MPI_Gather(sendBuffer,CHROMOSOME_LENGTH,MPI_INT,receiveBuffer,CHROMOSOME_LENGTH,MPI_INT,MASTER, MPI_COMM_WORLD);
	
		
	if(myRank==MASTER)
	{
	
	    printf("\n");
		printf( "+===================================+\n" );
	    printf( "| Genetic Algorithm Finished        | \n");
	    printf( "+===================================+\n" );

		     printf("The genetic algorithm is finished \n");
			 printf(" Best fitness is %f \n",best_fitness);
	 		 Chromosome *best_chromosomes=fromBufferToChromosomes(receiveBuffer,NODES_SIZE);
		     printf("Best chromosome(s) are : \n");// we could have multiple chromosomes with the same fitness
	          for(int i=0;i<NODES_SIZE;i++)
			  {
				  if(best_chromosomes[i].getFitness()==best_fitness)
					  best_chromosomes[i].print();
			  }
	
		     
	}
	
	
	
	delete generation;

	
MPI_Finalize();
		return 0;
}

int read_parameters_from_file()
{
	FILE *fp;
	int i, line;
	char buffer[1024];
	fp = fopen("tsp.config", "r");

    fgets(buffer, 1024, fp);
	
	if (fscanf(fp, "\t<TspLocationsFileName> %s </TspLocationsFileName>\n", cities_file) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the locations file name at line 1.\n");
		exit(0);
	}
     // cout<<cities_file<<endl;
    
	if (fscanf(fp, "\t<PopulationSize> %d </PopulationSize>\n", &POPULATION_SIZE) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the population size at line 2.\n");
		exit(0);
	}
     //cout<<POPULATION_SIZE<<endl;
	if (fscanf(fp, "\t<MigrationPercentage> %f </MigrationPercentage>\n", &MIGRATION_PERCENTAGE)<= 0) {
		printf("Illegal tsp paremeters file format. Expecting the max fitness size at line 3.\n");
		exit(0);
	}
	migration_size=POPULATION_SIZE*MIGRATION_PERCENTAGE;
	if (fscanf(fp, "\t<ScramblePercentage> %f </ScramblePercentage>\n", &SCRAMBLE_PERCENTAGE)<= 0) {
		printf("Illegal tsp paremeters file format. Expecting the scramble percentage at line 4.\n");
		exit(0);
	}
     //cout<<SCRAMBLE_PERCENTAGE<<endl;
	if (fscanf(fp, "\t<MutationProbability> %f </MutationProbability>\n", &MUTATION_PROBABILITY) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the mutation rate at line 5.\n");
		exit(0);
	}
     //cout<<MUTATION_PROBABILITY<<endl;
    if (fscanf(fp, "\t<CrossoverProbability> %f </CrossoverProbability>\n", &CROSSOVER_PROBABILITY) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the crossover probability  at line 6.\n");
		exit(0);
	}
	 //cout<<CROSSOVER_PROBABILITY<<endl;
     if (fscanf(fp, "\t<SimilarityRatio> %f </SimilarityRatio>\n", &SIMILARITY_RATIO) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the maximum similarity allowed at line 6.\n");
		exit(0);
	}
     //cout<<SIMILARITY_RATIO<<endl;
	if (fscanf(fp, "\t<ExecutionTimeSeconds> %d </ExecutionTimeSeconds>\n", &EXECUTION_TIME_SECONDS) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the execution time at line 7.\n");
		exit(0);
	}
     //cout<<EXECUTION_TIME_SECONDS<<endl;
	if (fscanf(fp, "\t<ExchangePeriodSeconds> %d </ExchangePeriodSeconds>\n", &EXCHANGE_PERIOD_SECONDS) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the exchange period at line 8.\n");
		exit(0);
	}
     //cout<<EXCHANGE_PERIOD_SECONDS<<endl;
	if (fscanf(fp, "\t<NodesList size='%d'>\n", &NODES_SIZE) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the nodes size at line 9.\n");
		exit(0);
	}
     //cout<<NODES_SIZE<<endl;
		
	nodes=new Node[NODES_SIZE];

	for(int i=0;i<NODES_SIZE;i++)
	{
		
	if (fscanf(fp, "\t\t<Node speed='%d' capacity='%d'> %s </Node>\n",&nodes[i].speed,&nodes[i].capacity,nodes[i].name) <= 0) 
	{
		printf("Illegal tsp paremeters file format. Expecting the node  at line %d.\n",i+10);
		exit(0);
	}
	//cout<<"++++++++++++++"<<endl;
    //cout<<"Name: "<<nodes[i].name<<endl;
	//cout<<"Speed: "<<nodes[i].speed<<endl;
	//cout<<"Capacity: "<<nodes[i].capacity<<endl;
	//cout<<"++++++++++++++"<<endl;
			
	}
	fclose(fp);

}

int read_cities_from_file()
{
	FILE *fp;
	int i, line;
	char buffer[1024];
	float x, y;
	int nb_cities;
        fp = fopen(cities_file, "r");
	

	for (i = 0; i < 4; i++) {
		fgets(buffer, 1024, fp);
	}

	if (fscanf(fp, "DIMENSION : %d", &nb_cities) == 0) {
		printf("Illegal tsp locations file format. Expecting the DIMENSION at line 5.\n");
		exit(0);
	}
	
       CHROMOSOME_LENGTH = nb_cities;
	for (i = 0; i < 2; i++) {
		fgets(buffer, 1024, fp);
	}

	cities = new City[nb_cities];
	rewind(fp);

	for (i = 0; i < 7; i++) {
		fgets(buffer, 1024, fp);
	}

	while (fscanf(fp, "%d %f %f", &line, &x, &y) > 0 && line <= nb_cities) {
		cities[line - 1].Id = line;
		cities[line - 1].X = x;
		cities[line - 1].Y = y;
#ifdef DEBUG
		printf("City %d %.4f %.4f\n", cities[line - 1].id, cities[line - 1].x, cities[line - 1].y);
#endif
	}

	fclose(fp);

	return nb_cities;
}

char* getProcessorName()
{
	//MPI_Get_processor_name(hostname,&len);
	return nodes[myRank].name;
	
}

void setSubPopulationsSizes()
{
	float max=0.0;
	for(int i=0;i<NODES_SIZE;i++)
		if(nodes[i].speed + nodes[i].capacity > max )
			max=nodes[i].speed + nodes[i].capacity ;
	for(int i=0;i<NODES_SIZE;i++)
	{
		nodes[i].population_size=POPULATION_SIZE*((nodes[i].speed+nodes[i].capacity)/max);
		if(nodes[i].population_size%2==1)
			nodes[i].population_size++;
		
	}
}
int getNodeIndex(char* name)
{
	for(int i=0;i<NODES_SIZE;i++)
		if(strcmp(nodes[i].name,getProcessorName())==0)
		   return i;
}

void printSubPopulationsSizes()
{
	for(int i=0;i<NODES_SIZE;i++)
		cout<<nodes[i].population_size<<endl;
}

void ChangeParameters(Generation *generation,int *generations_with_no_improvements )
{
	  if(*generations_with_no_improvements >= 1000 )
		  {
			  if(!options[0])
			  {
				  options[0]=true;
				    if(mutation==SCRAMBLE_MUTATION)
				  {
				    generation->useMutationOperation(swapping_mutation);
					  mutation=SWAP_MUTATION;
					  printf("Process %d : %s => Swap Mutation\n",myRank,options_names[0]);
				  }
				  else 
				  {
                    generation->useMutationOperation(scramble_mutate);
	                mutation=SCRAMBLE_MUTATION;
				     printf("Process %d : %s => Scramble Mutation\n",myRank,options_names[0]);
				  }
				  
				  
			  }
			  else if(!options[1])
			  { 
				  options[1]=true;
				  if(crossover==CYCLE_CROSSOVER)
				  {
					  generation->useCrossoverOperation(multipoint_crossover);
					  crossover=MULTIPOINT_CROSSOVER;
					    printf("Process %d : %s => Multipoint Crossover\n",myRank,options_names[1]);
				  }
				  else
				  {
					 generation->useCrossoverOperation(cyclic_crossover);
	                 crossover=CYCLE_CROSSOVER; 
					   printf("Process %d : %s => Cycle Crossover\n",myRank,options_names[1]);
				  }
				  
			
			  }
				 else if(!options[2])
			  { 
				  options[2]=true;
					 MUTATION_PROBABILITY+=0.05;
					 if(MUTATION_PROBABILITY >1)
					 MUTATION_PROBABILITY=1;
				 printf("Process %d : %s => %f\n",myRank,options_names[2],MUTATION_PROBABILITY);
				  
			  }
		     else if(mutation==SCRAMBLE_MUTATION  && !options[3])
			  { 
			   
				  options[3]=true;
				  SCRAMBLE_PERCENTAGE+=0.05;
				  if(SCRAMBLE_PERCENTAGE>1)
		          SCRAMBLE_PERCENTAGE=1;
				 printf("Process %d : %s => %f \n",myRank,options_names[3],SCRAMBLE_PERCENTAGE);
				  
			  }
		   else if(!options[4])
			  {
			   							   
				   options[4]=true;
				   CROSSOVER_PROBABILITY+=0.01;
			       if(CROSSOVER_PROBABILITY>1)
				   CROSSOVER_PROBABILITY=1;
				 printf("Process %d : %s => %f\n",myRank,options_names[4],CROSSOVER_PROBABILITY);
				  
			  }
		     
		     
			  else 
			  {
				  options[0]=options[1]=options[2]=options[3]=false;
				  
				  printf("Process %d : Tried all options. Now back to option 1\n",myRank);
			  }
				  
			  *generations_with_no_improvements =0;	
			  
		  }
}

int* fromChromosomesToBuffer(Chromosome* solutions, int n)
{
	int *buffer=new int[n*CHROMOSOME_LENGTH];
#pragma omp parallel for shared(n)
	for(int i=0;i<n;i++)
	{
#pragma omp parallel for shared(i,solutions,buffer,CHROMOSOME_LENGTH)
		for(int j=0;j<CHROMOSOME_LENGTH;j++)
		{
			buffer[i*CHROMOSOME_LENGTH+j]=solutions[i].genes[j];
		}
			
	}
		
		return buffer;
}


Chromosome* fromBufferToChromosomes(int* buffer, int n)
{
	Chromosome* chromosomes=new Chromosome[n];
#pragma omp parallel for shared(chromosomes,n,CHROMOSOME_LENGTH)
	for(int i=0;i<n;i++)
	{
		chromosomes[i]=Chromosome();
#pragma omp parallel for shared(i,chromosomes,buffer,CHROMOSOME_LENGTH)
		for(int j=0;j<CHROMOSOME_LENGTH;j++)
		{
			chromosomes[i].genes[j]=buffer[i*CHROMOSOME_LENGTH+j];
		}
			
	}
		
		return chromosomes;
}

			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
