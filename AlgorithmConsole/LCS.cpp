#include "pch.h"
#include "LCS.h"
#include <iostream>
using namespace std;

LCS::LCS(string first, string second)
{
	LCS::first = first;
	LCS::second = second;

	rowLength = first.length() + 1;
	colLength = second.length() + 1;

	mappings = new int*[rowLength];

	for (int i = 0; i < rowLength; i++) {
		mappings[i] = new int[colLength];
		mappings[i][0] = 0;
	}

	for (int i = 0; i < colLength; i++) {
		mappings[0][i] = 0;
	}
}


LCS::~LCS()
{
}

int LCS::FindLCS_Length()
{
	for (int i = 1; i < rowLength; i++)
	{
		for (int j = 1; j < colLength; j++)
		{
			if (first[i - 1] == second[j - 1]) {
				mappings[i][j] = mappings[i - 1][j - 1] + 1;
			}
			else {
				mappings[i][j] = mappings[i][j - 1] > mappings[i - 1][j] ? mappings[i][j - 1] : mappings[i - 1][j];
			}
		}
	}

	return mappings[rowLength - 1][colLength - 1];
}

string LCS::GetOneLCS()
{
	const int resultLength = mappings[rowLength - 1][colLength - 1];
	auto results = new char[resultLength + 1];
	int maxLength = colLength - 1;
	int index = resultLength - 1;
	for (int i = rowLength - 1; i > 0; i--)
	{
		for (int j = maxLength; j > 0; j--)
		{
			if (mappings[i][j] == mappings[i - 1][j - 1] + 1) {
				if (mappings[i][j] == mappings[i][j - 1]) {
					continue;
				}
				else if (mappings[i][j] == mappings[i - 1][j])
				{
					maxLength = j;
					break;
				}
				else
				{
					results[index--] = first[i - 1];
					maxLength = j - 1;
					break;
				}
			}
		}
	}

	results[resultLength] = '\0';
	return string(results);
}

int LCS::FindLCSubstring_Length()
{
	int maxLength = 0;
	for (int i = 1; i < rowLength; i++)
	{
		for (int j = 1; j < colLength; j++)
		{
			if (first[i - 1] == second[j - 1]) {
				mappings[i][j] = mappings[i - 1][j - 1] + 1;
				maxLength = maxLength > mappings[i][j] ? maxLength : mappings[i][j];
			}
			else
			{
				mappings[i][j] = 0;
			}
		}
	}

	return maxLength;
}

string LCS::GetOneLCSubstring(int length)
{
	char* results = new char[length + 1];
	results[length] = '\0';
	for (int i = rowLength - 1; i > 0; i--)
	{
		for (int j = colLength - 1; j > 0; j--)
		{
			if (mappings[i][j] == length) {
				for (int k = length - 1; k >= 0; k--) {
					results[k] = first[i - length + k];
				}
				return results;
			}
		}
	}

	return nullptr;
}
