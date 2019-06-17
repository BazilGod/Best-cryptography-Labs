#pragma once
#include <iostream>
#include <string>
#include <vector>

using namespace std;

//these two functions are used to find the next prime number
bool IsPrime(int number)
{

	if (number == 2 || number == 3)
		return true;

	if (number % 2 == 0 || number % 3 == 0)
		return false;

	int divisor = 6;
	while (divisor * divisor - 2 * divisor + 1 <= number)
	{

		if (number % (divisor - 1) == 0)
			return false;

		if (number % (divisor + 1) == 0)
			return false;

		divisor += 6;

	}

	return true;

}

int NextPrime(int a)
{

	while (!IsPrime(++a))
	{
	}
	//cout << "size is now : " << a << endl;
	return a;

}

template <class HashedObj>
class HashTable
{
public:
	explicit HashTable(const HashedObj & notFound, int size = 101);
	HashTable(const HashTable & rhs)
		: ITEM_NOT_FOUND(rhs.ITEM_NOT_FOUND),
		varray(rhs.varray), currentSize(rhs.currentSize) { }

	const int & find(const HashedObj & x) const; //returns the given object's positions
	void makeEmpty(); //replaces all elements in hash table with empty ones
	void insert(const HashedObj & x); // inserts element to next position
	HashedObj findAtPos(unsigned int x); //returns element in given position
	int curSize(); //returns current number of elements in table

private:

	vector<HashedObj> varray; //object vector
	int currentSize = 0; //current number of elements
	const HashedObj ITEM_NOT_FOUND;
	void rehash(); //rehashes the table, increasing size in case of lambda>1/2
};

template<class HashedObj>
HashTable<HashedObj>::HashTable(const HashedObj & notFound, int size) : ITEM_NOT_FOUND(notFound), varray(NextPrime(size))
{
	makeEmpty();
}


template<class HashedObj>
const int & HashTable<HashedObj>::find(const HashedObj & x) const
{
	for (int i = 0; i < varray.size(); i++)
	{
		if (varray.at(i) == x)
		{
			return i;
		}
	}
	return 0;
}

template <class HashedObj>
void HashTable<HashedObj>::insert(const HashedObj & x)
{
	if (find(x)<=0)
	{
		//cout << "inserting " << x << " at: " << currentSize << endl;
		varray[currentSize] = x;
		currentSize++;

		if (currentSize >= varray.size()/2 ) //rehash condition
		{
			rehash();
		}
	}
}

template<class HashedObj>
inline HashedObj HashTable<HashedObj>::findAtPos(unsigned int x)
{
	return varray.at(x);
}

template<class HashedObj>
inline int HashTable<HashedObj>::curSize()
{
	return currentSize;
}

template <class HashedObj>
void HashTable<HashedObj>::rehash()
{
	vector<HashedObj> oldArray = varray;

	// Create new double-sized, empty table
	varray.resize(NextPrime(2 * oldArray.size()));
	for (int j = 0; j < varray.size(); j++)
	{
		varray[j] = "";
	}
	// Copy table over
	currentSize = 0;
	for (int i = 0; i < oldArray.size(); i++)
	{
		insert(oldArray[i]);
	}
}

template<class HashedObj>
inline void HashTable<HashedObj>::makeEmpty()
{
	for (int i = 0; i < varray.size(); i++)
	{
		varray.at(i) = "";
	}
}