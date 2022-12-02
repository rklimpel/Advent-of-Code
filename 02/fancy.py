SCORE_MAP_1 = {"A X": 4, "A Y": 8, "A Z": 3, "B X": 1, "B Y": 5, "B Z": 9, "C X": 7, "C Y": 2, "C Z": 6}
SCORE_MAP_2 = {"A X": 3, "A Y": 4, "A Z": 8, "B X": 1, "B Y": 5, "B Z": 9, "C X": 2, "C Y": 6, "C Z": 7}

print('Task 1: ' + str(sum(SCORE_MAP_1[i] for i in open("input.txt", "r").read().split("\n"))))
print('Task 2: ' + str(sum(SCORE_MAP_2[i] for i in open("input.txt", "r").read().split("\n"))))