class Monkey():

    def __init__(self, line) -> None:
        self.name = line.split(':')[0]
        operation = line.split(':')[1][1:]
        if operation.isnumeric():
            self.number = int(operation)
        else:
            self.number = None
            self.operand_a = operation[0:4]
            self.operand_b = operation[7:12]
            self.operation = operation[5]

    def __str__(self) -> str:
        return str(self.__dict__)


lines = open("input.txt", "r").read().split('\n')
monkeys = {}
for line in lines:
    monkey = Monkey(line)
    monkeys[monkey.name] = monkey

def calc_monkey(monkey):
    if monkey.number != None:
        return monkey.number
    else:
        monkey_a = monkeys[monkey.operand_a]
        monkey_b = monkeys[monkey.operand_b]
        if monkey.operation == '+':
            return calc_monkey(monkey_a) + calc_monkey(monkey_b)
        elif monkey.operation == '-':
            return calc_monkey(monkey_a) - calc_monkey(monkey_b)
        elif monkey.operation == '*':
            return calc_monkey(monkey_a) * calc_monkey(monkey_b)
        elif monkey.operation == '/':
            return calc_monkey(monkey_a) / calc_monkey(monkey_b)

print(calc_monkey(monkeys['root']))

