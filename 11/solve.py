import math
import sys
sys.set_int_max_str_digits(9999999)

def fixNumber(i):
    return int(i.replace(' ',''))

class Monkey():

    def read(self, lines):
        self.id = int(lines[0].split(' ')[1].replace(':',''))
        self.items = list(map(fixNumber,lines[1][lines[1].index(':')+2:].split(',')))
        self.operation = '+' if '+' in lines[2] else '*'
        self.operationNumber = lines[2][lines[2].index(self.operation)+2:]
        self.testNumber = int(lines[3].split(' ')[-1])
        self.ifTrue = int(lines[4][-1])
        self.ifFalse = int(lines[5][-1])

    def __str__(self) -> str:
        return 'MONKEY ' + str(self.id) + '\n' \
            + 'items: ' + str(self.items) + '\n' \
            + 'operation: ' + self.operation + '\n' \
            + 'operationNumber: ' + str(self.operationNumber) + '\n' \
            + 'testNumber: ' + str(self.testNumber) + '\n' \
            + 'ifTrue: ' + str(self.ifTrue) + '\n' \
            + 'ifFalse: ' + str(self.ifFalse)


lines = open("input.txt", "r").read().split('\n')
monkeyCount = int(lines[0::7][-1].split(' ')[1].replace(':',''))
inspectionCount = [0 for _ in range(0,monkeyCount+1)]
monkeys = []

for i in range(0, monkeyCount +1):
    m = Monkey()
    m.read(lines[i*7:i*7+6])
    # print(m)
    monkeys.append(m)

for r in range(0,10000):
    for m in monkeys:
        #print(m)
        for i in m.items:
            inspectionCount[m.id] += 1

            if m.operationNumber == 'old':
                w = i * i if m.operation == '*' else i + i
            else: 
                w = i * int(m.operationNumber) if m.operation == '*' else i + int(m.operationNumber)

            # w = math.floor(w/3)

            if w % m.testNumber == 0: 
                monkeys[m.ifTrue].items.append(w)
                #print("Monkey " + str(m.id) + " throws item with worry level " + str(w) + " to Monkey " + str(m.ifTrue))
            else: 
                monkeys[m.ifFalse].items.append(w)
                #print("Monkey " + str(m.id) + " throws item with worry level " + str(w) + " to Monkey " + str(m.ifFalse))
            m.items = m.items[1:]
    # Print Round End Results
    # for m in monkeys:
        #print("Monkey " + str(m.id) + ": " + str(m.items))

    if r % 1 == 0:
        print(" === ROUND " + str(r) + " === ")
        for m in monkeys:
            print("Monkey " + str(m.id) + " inspected items " + str(inspectionCount[m.id]) + " times.")

def mult(l):
    result = 1
    for x in l: result = result * x
    return result

print((sorted(inspectionCount)[-2:]))
print(mult((sorted(inspectionCount)[-2:])))



# 485 too low