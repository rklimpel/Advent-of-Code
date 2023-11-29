import string

SCORE_MAP = dict(zip(string.ascii_letters, [item for item in range(1,53)]))
lines = open("input.txt", "r").read().split("\n")
groups = [lines[n:n+3] for n in range(0, len(lines), 3)] 
print(sum(SCORE_MAP[set(line[:int(len(line)/2)]).intersection(line[int(len(line)/2):]).pop()] for line in lines))
print(sum(SCORE_MAP[set(group[0]).intersection(group[1]).intersection(group[2]).pop()] for group in groups))