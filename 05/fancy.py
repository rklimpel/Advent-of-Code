config, instructions = [part.split("\n") for part in open("input.txt", "r").read().split("\n\n")]
stacks = [[] for i in range(max([int(s) for s in config[-1].split() if s.isdigit()]))]
for line in config[:-1]:
    for i, box in enumerate(line[1::4]):
        if box != ' ': stacks[i] += box
    
stack1, stack2 = stacks[:], stacks[:]
for line in instructions:
    n, src, dest = [int(s) for s in line.split() if s.isdigit()]
    stack1[src-1], stack1[dest-1] = stack1[src-1][n:], stack1[src-1][:n][::-1] + stack1[dest-1]
    stack2[src-1], stack2[dest-1] = stack2[src-1][n:], stack2[src-1][:n] + stack2[dest-1]

print('Task 1: ', ''.join(s[0] for s in stack1 if s))
print('Task 2: ', ''.join(s[0] for s in stack2 if s))