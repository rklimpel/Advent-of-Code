import json

LOGGING = False

def checkPair(pair, depth):
    a, b = pair
    if LOGGING: print(' ' * depth + '- ' + 'Compare ' + str(a) + ' vs ' + str(b))
    if not isinstance(a, list) and not isinstance(b, list):
        if a < b: 
            if LOGGING: print(' '*depth + '- Left side is smaller, so inputs are in the right order')
            return True 
        elif a > b: 
            if LOGGING: print(' '*depth + '- Right side is smaller, so inputs are not in the right order')
            return False
        else: 
            return None
    elif not isinstance(a, list) and isinstance(b, list):
        if LOGGING: print(' '*depth + '- Mixed types; convert left to ' + str([a]) + ' and retry comparison')
        return checkPair(([a],b), depth)
    elif isinstance(a, list) and not isinstance(b, list):
        if LOGGING: print(' '*depth + '- Mixed types; convert right to ' + str([b]) + ' and retry comparison')
        return checkPair((a,[b]), depth)
    elif isinstance(a, list) and isinstance(b, list): #both are lists
        for i in range(len(a)):
            if len(b) <= i: 
                if LOGGING: print(' '*depth + '- Right side ran out of items, so inputs are not in the right order')
                return False
            v = checkPair((a[i],b[i]), depth+1)
            if isinstance(v, bool) : return v
        if len(a) < len(b):
            if LOGGING: print(' '*depth + '- Left side ran out of items, so inputs are in the right order')
            return True
        elif len(b) == len(a):
            return None

lines = open("input.txt", "r").read().split('\n')
pairs = []
for line in lines:
    if '[' in line:
        pairs.append(json.loads(line))

sorted_pairs = []
sorted_pairs.append(pairs[0])
pairs = pairs[1:]
for i in range(len(pairs)):
    inserted = False
    for j in range(len(sorted_pairs)):
        is_smaller = checkPair((pairs[i],sorted_pairs[j]), 0)
        if is_smaller:
            sorted_pairs.insert(j, pairs[i])
            inserted = True
            break
    if inserted == False:
        sorted_pairs.append(pairs[i])

divider_packets = [[[2]],[[6]]]
divider_packets_indices = []

for i in range(len(divider_packets)):
    inserted = False
    for j in range(len(sorted_pairs)):
        is_smaller = checkPair((divider_packets[i],sorted_pairs[j]), 0)
        if is_smaller:
            sorted_pairs.insert(j, divider_packets[i])
            divider_packets_indices.append(j+1)
            inserted = True
            break
    if inserted == False:
        sorted_pairs.append(divider_packets[i])
        divider_packets_indices.append(len(sorted_pairs))

if LOGGING:
    print("Inputs:")
    for i in range(0, len(pairs)):
        print(str(i+1) + ': ' + str(pairs[i]))

    print("Sorted:")
    for i in range(0, len(sorted_pairs)):
        print(str(i+1) + ': ' + str(sorted_pairs[i]))

print(divider_packets_indices)
print(divider_packets_indices[0] * divider_packets_indices[1])

