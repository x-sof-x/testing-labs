USE CalcTestsDB;
GO

IF OBJECT_ID('dbo.RunEstimateTestCases', 'U') IS NOT NULL
    DROP TABLE dbo.RunEstimateTestCases;
GO

CREATE TABLE dbo.RunEstimateTestCases
(
    TestId            INT IDENTITY(1,1) PRIMARY KEY,
    TestName          NVARCHAR(100) NOT NULL,
    InputExpression   NVARCHAR(500) NOT NULL,   -- вже "форматований" вираз (як після Format), m/p замість унарних -/+
    ExpectedResult    NVARCHAR(500) NULL,       -- очікуваний результат, якщо помилка НЕ очікується
    IsErrorExpected   BIT NOT NULL DEFAULT(0)   -- 1 - очікуємо, що результат почнеться з '&'
);
GO

INSERT INTO dbo.RunEstimateTestCases (TestName, InputExpression, ExpectedResult, IsErrorExpected)
VALUES
    -- один операнд, без операторів: queue.Count == 1 -> повертається одразу
    (N'SingleOperand',         N'5',           N'5',   0),

    -- бінарні операції
    (N'Addition',              N'2+3',         N'5',   0),
    (N'Subtraction',           N'10-4',        N'6',   0),
    (N'Multiplication',        N'3*4',         N'12',  0),
    (N'Division',              N'10/2',        N'5',   0),
    (N'Modulo',                N'10%3',        N'1',   0),

    -- пріоритети операцій (RPN має врахувати */  раніше за +/-)
    (N'OperatorPriority',      N'2+3*4',       N'14',  0),

    -- дужки
    (N'Brackets',              N'(2+3)*4',     N'20',  0),
    (N'NestedBrackets',        N'((2+3)*(4-1))', N'15', 0),

    -- унарні оператори (вже замінені: m = унарний мінус, p = унарний плюс)
    (N'UnaryMinusOnNumber',    N'm5',          NULL,   1),  -- перевір фактичну поведінку CalcClass.IABS і онови ExpectedResult/IsErrorExpected
    (N'UnaryPlusOnNumber',     N'p5',          NULL,   1),  -- аналогічно для CalcClass.ABS

    -- помилкові/крайні випадки на рівні обчислення (не Format!)
    (N'DivisionByZero',        N'5/0',         NULL,   1),
    (N'ModuloByZero',          N'5%0',         NULL,   1),
    (N'OverflowMultiplication',N'9999999999*9999999999', NULL, 1),

    -- перевищення MAX_COUNT_OPERANDS (30) -> "&" + ERROR_08
    -- 31 операнд через '+' => 31 число + 30 операторів = 61 токен у черзі RPN
    (N'TooManyOperands',
     N'1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1',
     NULL, 1);
GO