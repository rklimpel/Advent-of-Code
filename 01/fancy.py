print('Task 1: ' + str(max(sum([int(c) for c in l.split("\n")]) for l in open("input.txt", "r").read().split("\n\n"))))
print('Task 2: ' + str(sum(sorted(list([sum([int(c) for c in l.split("\n")]) for l in open("input.txt", "r").read().split("\n\n")]))[-3:])))
