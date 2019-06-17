#include <iostream>
#include <string>
#include <fstream>
#include "HashTable.h"

using namespace std;

void init(HashTable<string>& table)
{
	for (int i = 0; i < 256; i++)
	{
		char c = (char)i;
		string str(1, c);
		table.insert(str);
	}
}

int main()
{
	string mainStr;
	HashTable<string> table("",500);
	init(table); //initializes the table, inserting chars 0-255

	//input and output files
	ifstream deneme("compout.txt");
	ofstream sonuc("decompout.txt");

	string previous_string;
	unsigned int code;

	//manually outputs the first char and sets it as the previous string for the second input character
	deneme >> code;
	sonuc << table.findAtPos(code);
	previous_string = table.findAtPos(code);

	//takes code numbers one by one
	while (deneme >> code) {
		if (code < table.curSize()) // if the given code reference is found in table
		{
			sonuc << table.findAtPos(code); //writes the string
			table.insert(previous_string + table.findAtPos(code)[0]); //inserts new string to table
		}
		else // if not found
		{
			sonuc << previous_string + previous_string[0]; //writes a new string according to algorithm
			table.insert(previous_string + previous_string[0]); //inserts the new string to table
		}
		previous_string = table.findAtPos(code); //Sets current string as previous one
	}

	//closes streams
	deneme.close();
	sonuc.close();

	return 0;
}