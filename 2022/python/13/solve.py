import json

def checkPair(pair, depth):
    a, b = pair
    print(' ' * depth + '- ' + 'Compare ' + str(a) + ' vs ' + str(b))
    if not isinstance(a, list) and not isinstance(b, list):
        if a < b: 
            print(' '*depth + '- Left side is smaller, so inputs are in the right order')
            return True 
        elif a > b: 
            print(' '*depth + '- Right side is smaller, so inputs are not in the right order')
            return False
        else: 
            return None
    elif not isinstance(a, list) and isinstance(b, list):
        print(' '*depth + '- Mixed types; convert left to ' + str([a]) + ' and retry comparison')
        return checkPair(([a],b), depth)
    elif isinstance(a, list) and not isinstance(b, list):
        print(' '*depth + '- Mixed types; convert right to ' + str([b]) + ' and retry comparison')
        return checkPair((a,[b]), depth)
    elif isinstance(a, list) and isinstance(b, list): #both are lists
        for i in range(len(a)):
            if len(b) <= i: 
                print(' '*depth + '- Right side ran out of items, so inputs are not in the right order')
                return False
            v = checkPair((a[i],b[i]), depth+1)
            if isinstance(v, bool) : return v
        if len(a) < len(b):
            print(' '*depth + '- Left side ran out of items, so inputs are in the right order')
            return True
        elif len(b) == len(a):
            return None

lines = open("input.txt", "r").read().split('\n')
pairs = []
for i in range(0, len(lines), 3):
    pairs.append((json.loads(lines[i]), json.loads(lines[i+1])))

success_indices = []
for i in range(len(pairs)):
    correct = checkPair(pairs[i], 0)
    print()
    if correct: success_indices.append(i+1)

print(success_indices)
print(sum(success_indices))