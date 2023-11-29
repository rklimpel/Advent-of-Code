import string
letters  = string.ascii_lowercase[:26] + string.ascii_uppercase[:26]
numbers = [item for item in range(1,53)]
scoreMap =dict(zip(letters, numbers))


lines = open("input.txt", "r").read().split("\n")
sum = 0
for line in lines:
    middle = int(len(line)/2)
    first = line[:middle]
    second = line[middle:]
    sum += scoreMap[set(first).intersection(second).pop()]

print(sum)