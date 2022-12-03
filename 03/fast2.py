import string
letters  = string.ascii_lowercase[:26] + string.ascii_uppercase[:26]
numbers = [item for item in range(1,53)]
scoreMap =dict(zip(letters, numbers))


lines = open("input.txt", "r").read().split("\n")
groups = [lines[n:n+3] for n in range(0, len(lines), 3)]

sum = 0
for group in groups:
    sum += scoreMap[set(group[0]).intersection(group[1]).intersection(group[2]).pop()]
    #sum += scoreMap[set(first).intersection(second).pop()]

print(sum)