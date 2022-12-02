# A -> Rock
# B -> Paper
# C -> Scissors

# X -> need to lose
# Y -> need a draw
# Z -> need a win

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

class Result(Enum):
    Loose = 0
    Draw = 3
    Win = 6

def convert_to_symbol(input):
    if input == "A": return Shape.Rock
    if input == "B": return Shape.Paper
    if input == "C": return Shape.Scissors

def convert_to_result(input):
    if input == "X": return Result.Loose
    if input == "Y": return Result.Draw
    if input == "Z": return Result.Win

def get_winning_symbol(symbol):
    if symbol == Shape.Rock: return Shape.Paper
    if symbol == Shape.Paper: return Shape.Scissors
    if symbol == Shape.Scissors: return Shape.Rock

def get_loosing_symbol(symbol):
    if symbol == Shape.Rock: return Shape.Scissors
    if symbol == Shape.Paper: return Shape.Rock
    if symbol == Shape.Scissors: return Shape.Paper

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
    enemy, result = line.split(' ')
    enemy = convert_to_symbol(enemy)
    result = convert_to_result(result)
    if result == Result.Loose:
        return get_loosing_symbol(enemy).value + result.value
    if result == Result.Draw:
        return enemy.value + result.value
    if result == Result.Win:
        return get_winning_symbol(enemy).value + result.value

lines = open("input.txt", "r").read().split("\n")
totalScore = 0
for line in lines:
    totalScore += calc_score(line)
print(totalScore)