from sympy import symbols, solve, sympify, solveset, Symbol

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

monkeys['root'].operation = '='
monkeys['humn'].number = 'x'

def build_calc_string(monkey):
    if monkey.number != None:
        return str(monkey.number)
    else:
        monkey_a = monkeys[monkey.operand_a]
        monkey_b = monkeys[monkey.operand_b]
        return '(' + build_calc_string(monkey_a) + monkey.operation + build_calc_string(monkey_b) + ')'

root_monkey = monkeys['root']
calc = build_calc_string(monkeys[root_monkey.operand_b]) + '-' + build_calc_string(monkeys[root_monkey.operand_a])

x = Symbol('x')
print("part2:", solve(sympify(calc), x)[0])