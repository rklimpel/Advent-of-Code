chars = open("input.txt", "r").read()
print('Task 1: ' + str([len(set(chars[i:i+4])) for i in range(0,(len(chars)-3))].index(4)+4))
print('Task 2: ' + str([len(set(chars[i:i+14])) for i in range(0,(len(chars)-14))].index(14)+14))