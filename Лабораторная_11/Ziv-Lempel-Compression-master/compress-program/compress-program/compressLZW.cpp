/* 
Author : Berke Ataç
CS300 Homework 2, Ziv-Lempel Algorith Compressor
14.11.2015
*/

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
	HashTable<string> table("",25);
	init(table); //initializes the table, inserting chars 0-255

	//input and output files
	ifstream deneme("compin.txt");
	ofstream sonuc("compout.txt");

	string curStr;
	char ch;

	//takes characters 1 by 1
	while (deneme >> ch >> std::noskipws) //without skipping spaces takes chars
	{
		curStr = curStr + ch;
		if ( table.find(curStr) == 0 ) //if not found
		{
			table.insert(curStr); //inserts current string to table
			curStr.erase(curStr.size() - 1); //deletes last char from string
			//cout << "writing " << curStr << endl;
			sonuc << table.find(curStr) << " "; //writes the position in file
			curStr = ch; //sets current string to current char
		}
	}
	sonuc << table.find(curStr) << " "; //writes the position in file
	//cout << "writing " << curStr << endl;


	deneme.close();
	sonuc.close();
	return 0;

}