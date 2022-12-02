# A -> Rock
# B -> Paper
# C -> Scissors

# X -> Rock
# Y -> Paper
# Z -> Scissors

# A,X -> +1
# B,Y -> +2
# C,Z -> +3

# Lose -> +0
# Draw -> +3
# Win -> +6

from enum import Enum

class Shape(Enum):
    Rock = 1
    Paper = 2
    Scissors = 3

def convert_to_symbol(input):
    if input == "X" or input == "A": return Shape.Rock
    if input == "Y" or input == "B": return Shape.Paper
    if input == "Z" or input == "C": return Shape.Scissors

def result_points(symbols):
    if symbols[0] == symbols[1]: 
        return 3
    if ((symbols[0] == Shape.Rock and symbols[1] == Shape.Paper) 
        or (symbols[0] == Shape.Paper and symbols[1] == Shape.Scissors)
        or (symbols[0] == Shape.Scissors and symbols[1] == Shape.Rock)):
        return 6
    else: 
        return 0

def calc_score(line):
    symbols = [convert_to_symbol(s) for s in line.split(' ')]
    return result_points(symbols) + symbols[1].value


lines = open("input.txt", "r").read().split("\n")
totalScore = 0
for line in lines:
    totalScore += calc_score(line)
print(totalScore)