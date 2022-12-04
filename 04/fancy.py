data = [list(map(int, c)) for c in [l.split(',') for l in open('input.txt').read().replace("-",",").split()]]
print('Task 1: ' + str(sum(1 for a, b, c, d in data if a <= c <= d <= b or c <= a <= b <= d)))
print('Task 2: ' + str(sum(1 for a, b, c, d in data if a <= d and c <= b)))