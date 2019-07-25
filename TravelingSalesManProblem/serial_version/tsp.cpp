/* Serial version for traveller salesman problem using genetic algorithm
* Solved by Abdullah Haidar 
* Genetic Operations used:
* ==> Selection : Roulette
* ==> Crossover : Cyclic  or  Multippoint
* ==> Mutation : Swapping or Scramble
* -------------------------------------------------------------------------
*/



#include "tsp.h"
#include <iostream>
#include <limits>
#include <assert.h>


using namespace std;
enum genetic_operator { MULTIPOINT_CROSSOVER,
			    CYCLE_CROSSOVER,
			    SWAP_MUTATION,
			    SCRAMBLE_MUTATION
			  };
char cities_file[255]="";
City *cities;
char options_names[][255]={"Change Mutation Operator",
				           "Change Crossover Operator",
				           "Change Mutation Rate",
				           "Change Scramble Rate",
				           "Change Crossover rate"
					      };
bool options[5]={0,0,0,0,0};


int read_parameters_from_file();
int read_cities_from_file();
void ChangeParameters(Generation*,int*);
genetic_operator crossover,mutation;

int main(int argc, char *argv[])
{
	
	Chromosome *best_chromosome;
	
	read_parameters_from_file();
    int nb_cities = read_cities_from_file();
    distances = Matrix(cities, nb_cities);
	

	Generation *generation = new Generation();
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
    
     while(time_passed < EXECUTION_TIME_SECONDS)
       { 			 
		generation->nextPopulation();
		generations++;
		double newFitness = generation->getBestFitness();
		if(newFitness > bestFitness)
		{
			bestFitness = newFitness;
			printf("Best fitness for now : %f\n",bestFitness);
			generations_with_no_improvements = 0;
			 
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
		time_passed = (float)(clock() - start) / CLOCKS_PER_SEC;
         
	}
       
	
 

	
	best_chromosome=generation->bestChromosone;
     bestFitness=best_chromosome->getFitness();
	    printf("\n");
		printf( "+===================================+\n" );
	    printf( "| Genetic Algorithm Finished        | \n");
	    printf( "+===================================+\n" );
            printf("Number of generations : %d \n",generations);
	    printf(" Best fitness is %f \n",bestFitness);
	    printf("Best chromosome is : \n");
	    best_chromosome->print();
			
	
	 delete generation;
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
	
	if (fscanf(fp, "\t<ScramblePercentage> %f </ScramblePercentage>\n", &SCRAMBLE_PERCENTAGE)<= 0) {
		printf("Illegal tsp paremeters file format. Expecting the scramble percentage at line 3.\n");
		exit(0);
	}
     //cout<<SCRAMBLE_PERCENTAGE<<endl;
	if (fscanf(fp, "\t<MutationProbability> %f </MutationProbability>\n", &MUTATION_PROBABILITY) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the mutation rate at line 4.\n");
		exit(0);
	}
     //cout<<MUTATION_PROBABILITY<<endl;
    if (fscanf(fp, "\t<CrossoverProbability> %f </CrossoverProbability>\n", &CROSSOVER_PROBABILITY) <= 0) {
		printf("Illegal tsp paremeters file format. Expecting the crossover probability  at line 5.\n");
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

void ChangeParameters(Generation *generation,int *generations_with_no_improvements )
{
	  if(*generations_with_no_improvements >= 100 )
		  {
			  if(!options[0])
			  {
				  options[0]=true;
				  if(mutation==SCRAMBLE_MUTATION)
				  {
				    generation->useMutationOperation(swapping_mutation);
					  mutation=SWAP_MUTATION;
					  printf("%s => Swap Mutation\n",options_names[0]);
				  }
				  else 
				  {
                    generation->useMutationOperation(scramble_mutate);
	                mutation=SCRAMBLE_MUTATION;
				    printf("%s => Scramble Mutation\n",options_names[0]);
				  }
				  
				  
			  }
			  else if(!options[1])
			  { 
				  options[1]=true;
				  if(crossover==CYCLE_CROSSOVER)
				  {
					  generation->useCrossoverOperation(multipoint_crossover);
					  crossover=MULTIPOINT_CROSSOVER;
					   printf("%s => Multipoint Crossover\n",options_names[1]);
				  }
				  else
				  {
					 generation->useCrossoverOperation(cyclic_crossover);
	                 crossover=CYCLE_CROSSOVER; 
					  printf("%s => Cycle Crossover\n",options_names[1]);
				  }
				  
				  
				  
			  }
				 else if(!options[2])
			  { 
				  options[2]=true;
				  MUTATION_PROBABILITY+=0.05;
					 if(MUTATION_PROBABILITY >1)
						 MUTATION_PROBABILITY=1;
				 printf("%s => %f\n",options_names[2],MUTATION_PROBABILITY);
				  
			  }
		     else if(mutation==SCRAMBLE_MUTATION && !options[3])
			  { 
			   
				  options[3]=true;
				  SCRAMBLE_PERCENTAGE+=0.05;
				 if(SCRAMBLE_PERCENTAGE>1)
					 SCRAMBLE_PERCENTAGE=1;
				 printf("%s => %f \n",options_names[3],SCRAMBLE_PERCENTAGE);
				  
			  }
		   else if(!options[4])
			  {
			   							   
				  options[4]=true;
				  CROSSOVER_PROBABILITY+=0.01;
			   if(CROSSOVER_PROBABILITY>1)
				   CROSSOVER_PROBABILITY=1;
				 printf("%s => %f\n",options_names[4],CROSSOVER_PROBABILITY);
				  
			  }
		     
		     
			  else 
			  {
				  options[0]=options[1]=options[2]=options[3]=false;
				  
				  printf("Tried all options. Now back to option 1\n");
			  }
				  
			  *generations_with_no_improvements =0;	
			  
		  }
}


			
			
			
			
			
			
			
