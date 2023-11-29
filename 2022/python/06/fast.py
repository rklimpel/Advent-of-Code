chars = open("input.txt", "r").read()
for i in range(0,(len(chars)-3)):
    if len(set(chars[i:i+4])) == 4:
        print(i+3+1)
        print(chars[i:i+4])
        break

for i in range(0,(len(chars)-13)):
    if len(set(chars[i:i+14])) == 14:
        print(i+13+1)
        print(chars[i:i+14])
        break