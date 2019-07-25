#pragma once
#include <string>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <math.h>
#include <iostream>
#include <assert.h>
#include <algorithm>
#include <sstream>
#include <cstring>
#include <fstream>
#include <random>       
#include <chrono>  
#define MASTER 0

int CHROMOSOME_LENGTH;
int POPULATION_SIZE;
float SCRAMBLE_PERCENTAGE;
int EXECUTION_TIME_SECONDS;
float SIMILARITY_RATIO;
float CROSSOVER_PROBABILITY ;//=0.9
float MUTATION_PROBABILITY ;// =0.02


double random_with_maximum(double max);
double random_without_maximum(double max);

using namespace std;



class City
{
public:
	int Id;
	float X;
	float Y;

	float distance(City city) {
		return sqrt(pow(X - city.X, 2) + pow(Y - city.Y, 2));
	}

};

class Matrix
{
	//The following implementaion of the distance matrix is more efficient than declaring
	// an array of pointers.

public:
	int dimension = 0;
	float * distances;
	Matrix(City cities[], int n)
	{
	  
		distances = new float[n*n];
		dimension = n;
 
		for (int i = 0; i < n; i++)
		
			for (int j = i; j < n; j++)
				distances[i + dimension * j] =distances[j + dimension * i] = cities[i].distance(cities[j]);
	}
	float get(int i, int j)
	{
		    
			return distances[i + dimension * j];
	
	}

	void print()
	{
		printf("\n\t");
		for (int i = 0; i < dimension; ++i) {
			printf("|%d|\t\t", i);
		}
		for (int i = 0; i < dimension; ++i) {
			printf("\n");
			printf("|%d|\t", i);
			for (int j = 0; j < dimension; j++) {
				printf("%.4f\t", get(i, j));
			}
		}
	}


} distances(nullptr,0);;

class Chromosome
{
public:
	int  *genes;
	double distance;
	Chromosome() : genes(new int[CHROMOSOME_LENGTH]) {}
	
	double calculateDistance()
	{
		distance = 0;

		for (int i = 0; i < CHROMOSOME_LENGTH - 1; i++) {
			if((genes[i + 1] - 1)>= 0 && (genes[i] - 1)>=0)
			distance += distances.get(genes[i]-1, genes[i+1]-1);
			
		}
		
		distance+=distances.get(genes[CHROMOSOME_LENGTH - 1]-1, genes[0]-1);
		return distance;
		
	}
	
	double getFitness() 
    {
	return 1/calculateDistance();
    }
	
	void shuffle()
   {
	for(int i = 0; i < CHROMOSOME_LENGTH; i++)
	{
		genes[i] = i+1;
	}

	
	for(int  i = CHROMOSOME_LENGTH-1; i > 0; i--)
	{
		int random = (int)random_with_maximum(i);
		int temp = genes[i];
		genes[i] = genes[random];
		genes[random] = temp;
	}
   }
   int compare(Chromosome* chromosome)
	{
	   float count=0.0f;
	   int *pos1=new int[CHROMOSOME_LENGTH+1],*pos2=new int[CHROMOSOME_LENGTH+1];

	   for(int i=0;i<CHROMOSOME_LENGTH;i++)
	   {
		pos1[genes[i]]=i;
		pos2[chromosome->genes[i]]=i;
		 
	   }

	   for(int i=0;i<CHROMOSOME_LENGTH;i++)
	   {
	     int current1=i; // the current index in chromosome 1
		 int current2=pos2[genes[current1]]; // move current2 to the position of genes[current1] in chromosome 2
		 int gene1;
		 int gene2;
		 
		 if(current1==CHROMOSOME_LENGTH-1)
		 		gene1=genes[0];
		  else
		      gene1=genes[current1+1]; // move gene1 to the next gene in chromosome 1
			  
	     if(current2==CHROMOSOME_LENGTH-1)
		      gene2=chromosome->genes[0];
		  else 
		      gene2=chromosome->genes[current2+1];   // move to the next position in chromosome 2
			  
		 
		 if(gene1==gene2)
		 {
		 //cout<<"gene1 :" <<gene1<<endl;
		 //cout<<"gene2 :"<<gene2 <<endl;
		 count++;
		
		 }
		   
		 	      
	   }
	   delete(pos1);
	   delete(pos2);
	    return count;
	}
	
	void print()
   {
	stringstream path;
	for(int gene = 0; gene < CHROMOSOME_LENGTH; ++gene)
	{
		if(gene != 0)
		{
			path << ",";
		}
		path << genes[gene];
	}
	cout<< path.str()<<endl;
   }
};

class Generation

{
	public:
		Generation();
		Chromosome * (*select)(Chromosome*) ;
		void (*crossover)(Chromosome*,Chromosome*,Chromosome*,Chromosome*) ;
		void (*mutate)(Chromosome*) ;
        Chromosome * solutions, *newPopulation,*migratedPopulation;
		Chromosome * bestChromosone;
		int newPopulationCount;
        void useSelectionOperation(Chromosome* (*f)(Chromosome*));
		void useCrossoverOperation(void (*f)(Chromosome*,Chromosome*,Chromosome*,Chromosome*));
		void useMutationOperation(void (*f)(Chromosome*));
		void nextPopulation();
		void addMigratedPopulation(int);
		double getBestFitness() ;
		string getBestPathString() ;
		double getLowestTotalDistance() ;
		double getAverageDistance() ;
		static bool areChromosonesEqual(Chromosome* chromosoneA, Chromosome* chromosoneB);
		bool hasDuplicate(Chromosome*, int);
		void copyToNewPopulation(Chromosome* chromosone, int index);
		Chromosome* mostFit(int);

	
};


Generation::Generation() :  solutions(new Chromosome[POPULATION_SIZE]), newPopulation(new Chromosome[POPULATION_SIZE])
{
	
	srand((unsigned int)(time(NULL)+rand()));

	for(int  i = 0; i < POPULATION_SIZE; i++)
	{
		(solutions + i)->shuffle();
	}
}


double Generation::getBestFitness() 
{
	return bestChromosone->getFitness();
}

double Generation::getAverageDistance()
{
	double distance = 0;

	for(int i = 0; i < POPULATION_SIZE; ++i)
	{
		distance += (solutions + i)->calculateDistance();
	}
	return distance/POPULATION_SIZE;
}

string Generation::getBestPathString()
{
	stringstream path;
	for(int gene = 0; gene < CHROMOSOME_LENGTH; ++gene)
	{
		if(gene != 0)
		{
			path << ",";
		}
		path << bestChromosone->genes[gene];
	}
	return path.str();
}

double Generation::getLowestTotalDistance() 
{
	return bestChromosone->calculateDistance();
}

void Generation::nextPopulation()
{
	//cout<<" Start next population ...  "<<endl;
	
	double fitness[POPULATION_SIZE];
	/* Fill an array with a fitness score for each chromosone,
	 * the index of a score corresponds with the chromosome's index in solutions[index]
	 */
	//cout<<" 	Start fitness calaculation ...  "<<endl;

	for(int i = 0; i < POPULATION_SIZE; ++i)
	{
		fitness[i] = (solutions + i)->getFitness();
	}
	//cout<<" 	End fitness calculation .  "<<endl;
	
	/* Use elitism, find and copy over the two best chromosones to the new population */
	int elite1 = 0, elite2 = 0;
	/* find the best solution */
	//cout<<" 	Finding elite 1 ...  "<<endl;
	elite1 = max_element(fitness, fitness + POPULATION_SIZE) - fitness;
	this->bestChromosone = solutions+elite1;
    //cout<<" 	Elite 1 is  at "<<elite1<<endl;
	double highestFitness = 0;
	/* Find the second best solution */
	//cout<<" 	Finding elite 2 ...  "<<endl;
	

	
	for(int i = 0; i < POPULATION_SIZE; ++i)
	{
		if(i!= elite1 && fitness[i] > highestFitness)
		{
			highestFitness = fitness[i];
			elite2 = i;
		}
	}
	 //cout<<" 	Elite 2 is  at "<<elite2<<endl;

	/* Keep track of how many chromosones exists in the new population */
	int offspringCount = newPopulationCount;
	/* Copy over the two best solutions to the new population */
	 //cout<<" 	Copying elite 1 ... "<<elite1<<endl;
	copyToNewPopulation(solutions + elite1, offspringCount);
	++offspringCount;
	 //cout<<" 	Copying elite 2 ... "<<endl;
	copyToNewPopulation(solutions + elite2, offspringCount);
	++offspringCount;
	 //cout<<" 	Copy ended. "<<endl;

	//cout<<" 	Creating rest of population. "<<endl;
	/* Create the rest of the new population, break this loop when the new population is complete */
	while(true)
	{
		Chromosome * parentA;
		Chromosome * parentB;
		//cout<<" 		Finding A ... "<<endl;
		parentA = select(solutions);
		
		//cout<<" 		Finding B ... "<<endl;
		parentB = select(solutions);
		//cout<<" 		Making sure A and B are different  ... "<<endl;
		while (parentB == parentA)
		{
			parentB = select(solutions);
		}
		//cout<<" 		A and B are different ... "<<endl;
		Chromosome * offspringA=new Chromosome();
		Chromosome * offspringB=new Chromosome();
		//cout<<" 		Crossing A and B ... "<<endl;
		
		crossover(parentA, parentB, offspringA, offspringB);
		
		//cout<<" 		Crossing ended ... "<<endl;
		mutate(offspringA);
		mutate(offspringB);
		
		/* Add to new population if an equal chromosone doesn't exist already */
		if(!hasDuplicate(offspringA, offspringCount))
		{
			copyToNewPopulation(offspringA, offspringCount);
			++offspringCount;
		}
		/* We need to check if the new population is filled */
		if(offspringCount == POPULATION_SIZE)
		{
			break;
		}
		if(!hasDuplicate(offspringB, offspringCount))
		{
			copyToNewPopulation(offspringB, offspringCount);
			++offspringCount;
		}
		/* Check again so that we don't accidentaly write all over the heap and have to spend an evening wondering why the heap is corrupt... :) */
		if(offspringCount == POPULATION_SIZE)
		{
			break;
		}
	}
    //cout<<" 	New population obtained. "<<endl;
	/*
	 * We now have a new population,
	 * now it needs to replace the current population
	 * so that we don't go through the same population every time we run this function
	 */

	for(int chromosoneIndex = 0; chromosoneIndex < POPULATION_SIZE; ++chromosoneIndex)
	{
		std::memcpy(solutions+chromosoneIndex, newPopulation+chromosoneIndex, sizeof(int) * CHROMOSOME_LENGTH);
	}
}

bool Generation::hasDuplicate(Chromosome* chromosome, int populationCount)
{
	/* Iterate throught each chromosone in newPopulation and compare them gene by gene */
	int ratio=CHROMOSOME_LENGTH * SIMILARITY_RATIO;
	bool result=false;

	for(int i = 0; i < populationCount;i++)
	{		
		if(chromosome->compare(solutions+i) >= ratio)
		   result=false;
	}

	return false;
}

void Generation::addMigratedPopulation(int n)
{

	for(int i=0;i<n;i++)
	{
		if(i<POPULATION_SIZE){	
		newPopulation[i] = *(migratedPopulation+i);
	     newPopulationCount++;
		}
		
	}
}

void swapping_mutation(Chromosome* chromosome)
{
	/* 0.0 <= random <= 1 */
	{
		double random = random_with_maximum(1);
		/* Nope, didn't happen */
		if(random > MUTATION_PROBABILITY)
		{
			return;
		}
	}

	int tmp;
	int random1 = (int)random_without_maximum(CHROMOSOME_LENGTH);
	int random2 = (int)random_without_maximum(CHROMOSOME_LENGTH);
	while(random1 == random2)
	{
		random2 = (int)random_without_maximum(CHROMOSOME_LENGTH);
	}

	tmp = chromosome->genes[random1];
	chromosome->genes[random1] = chromosome->genes[random2];
	chromosome->genes[random2] = tmp;

}
void scramble_mutate(Chromosome* chromosome)
{
	
		double random = random_with_maximum(1);
		/* Nope, didn't happen */
		if(random > MUTATION_PROBABILITY)
		{
			return;
		}
	
		int start=0,end;
		start = std::rand() % (int)(CHROMOSOME_LENGTH*(1-SCRAMBLE_PERCENTAGE));
	      unsigned seed = (unsigned)(time(NULL) + rand());
		end =start+ CHROMOSOME_LENGTH * SCRAMBLE_PERCENTAGE;
		std::shuffle(chromosome->genes+start,chromosome->genes+end, std::default_random_engine(seed));
		

}

void repairOffspring(Chromosome* offspringToRepair, int missingIndex,Chromosome*  other)
{
	/* Iterate through the other offspring until we find an element which doesn't exist in the offspring we are repairing */
	for(int patchIndex = 0; patchIndex < CHROMOSOME_LENGTH; ++patchIndex)
	{
		/* Look for other[patchIndex] in offspringToRepair */
		int *missing = find(offspringToRepair->genes, offspringToRepair->genes + CHROMOSOME_LENGTH, other->genes[patchIndex]);

		/* The element at other[patchIndex] is missing from offspringToRepair */
		if(missing == (offspringToRepair->genes + CHROMOSOME_LENGTH))
		{
			////cout << "1:" << offspringToRepair[missingIndex] << endl;
			offspringToRepair->genes[missingIndex] = other->genes[patchIndex];
			////cout << "2:" << offspringToRepair[missingIndex] << endl;
			break;
		}
	}
}
void multipoint_crossover(Chromosome* parentA, Chromosome* parentB, Chromosome * offspringA, Chromosome * offspringB)
{
	{
		/* There is a chance we don't perform a crossover,
		 * in that case the offspring is a copy of the parents
		 */
		/* 0.0 <= random <= 1 */
			//cout<<" 			Finding random number ... "<<endl;
		double random = random_with_maximum(1);
		//cout<<" 			Random number is  "<<random<<endl;
		/* The offspring is a copy of their parents */
		if(random > CROSSOVER_PROBABILITY)
		{
			//cout<<" 			A and B will not be crossed ... "<<endl;
			memcpy(offspringA->genes, parentA->genes, sizeof(int) * CHROMOSOME_LENGTH);
			memcpy(offspringB->genes, parentB->genes, sizeof(int) * CHROMOSOME_LENGTH);
			//cout<<" 			A and B copied to offsprings . "<<endl;
			return;
		}
	}
	/* Perform multi-point crossover to generate offspring */

	//cout<<" 			A and B will  be crossed ... "<<endl;	
	/* 0 <= cuttOffIndex <= CHROMOSOME_LENGTH */
	int cuttOffIndex1 = (int)random_with_maximum(CHROMOSOME_LENGTH);
	int cuttOffIndex2 = (int)random_with_maximum(CHROMOSOME_LENGTH);
	//cout<<" 			Random index 1 is   "<<cuttOffIndex1<<endl;
	//cout<<" 			Random index 2 is   "<<cuttOffIndex2<<endl;
	
	
	while(cuttOffIndex2 == cuttOffIndex1)
	{    //cout<<" 			Findind different index 2  "<<endl;	
		cuttOffIndex2 = (int)random_without_maximum(CHROMOSOME_LENGTH);
	     //cout<<" 			Try random index 2  "<<cuttOffIndex2<<endl;	
	}

	unsigned int start;
	unsigned int end;
	if(cuttOffIndex1 < cuttOffIndex2)
	{
		start = cuttOffIndex1;
		end = cuttOffIndex2;
	}
	else
	{
		start = cuttOffIndex2;
		end = cuttOffIndex1;
	}
	//cout<<" 			Copy start index  "<<start<<endl;	
	//cout<<" 			Copy end index  "<<end<<endl;
	/* Offspring A is initially copy of parent A */
	memcpy(offspringA->genes, parentA->genes, sizeof(int) * CHROMOSOME_LENGTH);
	//cout<<" 			offspringA initialized  "<<end<<endl;
	/* Offspring B is initially copy of parent B */
	memcpy(offspringB->genes, parentB->genes, sizeof(int) * CHROMOSOME_LENGTH);

	/* Put a sequence of parent B in offspring A */
	memcpy(offspringA->genes + start, parentB->genes + start, sizeof(int) * (end - start));
	/* Put a sequence of parent A in offspring B */
	memcpy(offspringB->genes + start, parentA->genes + start, sizeof(int) * (end - start));
	
     //cout<<" 			Copy ended  "<<start<<endl;
	/* Mark collisions in offspring with -1*/

	for(int cityIndex = 0; cityIndex  < CHROMOSOME_LENGTH; ++cityIndex)
	{
		/* Index is part of the parent sequence */
		if((cityIndex  >= start && cityIndex  < end)) {
			/* Do nothing, we want to keep this sequence intact */
		}
		else
		{
			/* Check if the item at cityIndex also occurs somewhere in the copied substring */

			for(int substringIndex = start; substringIndex < end; ++substringIndex)
			{
				/* A duplicate, mark it */
				if(offspringA->genes[cityIndex] == offspringA->genes[substringIndex])
				{
					offspringA->genes[cityIndex] = -1;
				}
				if(offspringB->genes[cityIndex] == offspringB->genes[substringIndex])
				{
					offspringB->genes[cityIndex] = -1;
				}
			}
		}

	}

	/*
	* Go through the offspring,
	* if an element is marked we fill the hole with an element from the other offspring
	*/

	for(int offspringIndex = 0; offspringIndex < CHROMOSOME_LENGTH; ++offspringIndex)
	{
		/* There is a hole here */
		if(offspringA->genes[offspringIndex] == -1)
		{
			repairOffspring(offspringA, offspringIndex, offspringB);
		}
		if(offspringB->genes[offspringIndex] == -1)
		{
			repairOffspring(offspringB, offspringIndex, offspringA);
		}
	}
}
void cyclic_crossover(Chromosome* parent1, Chromosome* parent2, Chromosome* child1, Chromosome* child2)
{
	bool flip = false;
	int count = CHROMOSOME_LENGTH;
	int cycle_start = parent1->genes[0];
	int i = 0;
    bool *visited = new bool[CHROMOSOME_LENGTH] {false};
	int *pos1= new int[CHROMOSOME_LENGTH+1],*pos2 = new int[CHROMOSOME_LENGTH+1];

	for(int i=0;i<CHROMOSOME_LENGTH;i++)
	{
		pos1[parent1->genes[i]]=i;
		pos2[parent2->genes[i]]=i;
	}
 
	for (int k = 1; k < CHROMOSOME_LENGTH+1; k++)
	{
		child1->genes[k]=k;
		child2->genes[k]=k;
	}
	while (count > 0)
	{
		do
		{

			if (!flip)
			{
				child1->genes[i]=parent1->genes[i];
				child2->genes[i]=parent2->genes[i];
			}
			else
			{
				child1->genes[i]=parent2->genes[i];
				child2->genes[i]=parent1->genes[i];
			}

		 
			visited[i] =true;
			i = pos1[parent2->genes[i]];
			count--;

		} while (i>=0 && parent1->genes[i] != -1 && parent1->genes[i] != cycle_start && count > 0);


		visited[i] = true;
		i = 0;
		while (i < CHROMOSOME_LENGTH &&  visited[i])
			i++;
		if (i == CHROMOSOME_LENGTH)
			return;
		cycle_start = parent1->genes[i];
		flip = !flip;
	}

	delete(pos1);
	delete(pos2);
	delete(visited);
}


void Generation::copyToNewPopulation(Chromosome* chromosome, int index)
{
	assert(index < POPULATION_SIZE && "Index out of bounds");
	
		newPopulation[index] = *chromosome;
	     newPopulationCount++;
	

}
void Generation::useSelectionOperation(Chromosome* (*f)(Chromosome*))
{
    select=f;	     
}
void Generation::useCrossoverOperation(void (*f)(Chromosome*,Chromosome*,Chromosome*,Chromosome*))
{
	crossover=f;
}

void Generation::useMutationOperation(void (*f)(Chromosome*))
{
	mutate=f;
}

Chromosome* Generation::mostFit(int n)
{
	Chromosome* max =new Chromosome[n];
 
	for(int i=0;i<n;i++)
		max[i].distance=INT64_MAX;

	for(int i=0;i<POPULATION_SIZE;i++)
	{
		for(int j=0;j<n;j++)
		{
			if(solutions[i].distance>max[j].distance)
				break;
			if(solutions[i].distance<max[j].distance)
			{
				if(j>0)
					max[j-1]=max[j];
				max[j]=solutions[i];
				
			}
		}
		
	}
	return max;
}
Chromosome* rouletteSelection(Chromosome * population) 
{
	double sum = 0;
	/* Calculate sum of all chromosome fitnesses in population */
	double *fitness=new double[POPULATION_SIZE];
 
	for(int i = 0; i < POPULATION_SIZE; ++i)
	{
		fitness[i]=1/population[i].distance;
		sum += fitness[i];
	}

	/* 0.0 <= random <= sum */
	double random = random_with_maximum(sum);

	sum = 0;
	/* Go through the population and sum fitnesses from 0 to sum s. When the sum s is greater or equal to r; stop and return the chromosome where you are */
	for(int i = 0; i < POPULATION_SIZE; ++i)
	{
		sum += fitness[i];
		if(sum >= random)
		{
			return population+i;
		}
	}
	assert(false && "A chromosone should have been picked by now");
	delete(fitness);
	return(NULL);
}


double random_with_maximum(double max)
{
	return ((double)rand() * max) / (double)RAND_MAX;
}

double random_without_maximum(double max)
{
	return ((double)rand() * max) / ((double)RAND_MAX + 1);
}

