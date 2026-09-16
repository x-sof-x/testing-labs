using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AnalaizerClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class RunEstimateTests
    {
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=CalcTestsDB;Integrated Security=true;";

        private static List<TestCase> _testCases;

        public class TestCase
        {
            public string TestName { get; set; }
            public string InputExpression { get; set; }
            public string ExpectedResult { get; set; }
            public bool IsErrorExpected { get; set; }

            public override string ToString() => TestName;
        }

        [ClassInitialize]
        public static void LoadTestData(TestContext context)
        {
            _testCases = new List<TestCase>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                const string query =
                    "SELECT TestName, InputExpression, ExpectedResult, IsErrorExpected " +
                    "FROM dbo.RunEstimateTestCases";

                using (var command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        _testCases.Add(new TestCase
                        {
                            TestName = reader.GetString(0),
                            InputExpression = reader.GetString(1),
                            ExpectedResult = reader.IsDBNull(2) ? null : reader.GetString(2),
                            IsErrorExpected = reader.GetBoolean(3)
                        });
                    }
                }
            }

            Assert.IsTrue(_testCases.Count > 0, "У таблиці RunEstimateTestCases немає тестових даних.");
        }

        public static IEnumerable<object[]> GetTestCases()
        {
            if (_testCases == null)
                LoadTestData(null);

            foreach (TestCase tc in _testCases)
                yield return new object[] { tc };
        }

        [TestMethod]
        [DynamicData(nameof(GetTestCases), DynamicDataSourceType.Method)]
        public void RunEstimate_FromDatabase(TestCase testCase)
        {
            AnalaizerClass.expression = testCase.InputExpression;

            string actual = AnalaizerClass.RunEstimate();

            if (testCase.IsErrorExpected)
            {
                Assert.IsTrue(
                    actual.StartsWith("&"),
                    $"[{testCase.TestName}] Очікувалась помилка (символ '&'), але отримано: '{actual}'");
            }
            else
            {
                Assert.AreEqual(
                    testCase.ExpectedResult,
                    actual,
                    $"[{testCase.TestName}] Вираз '{testCase.InputExpression}' дав неправильний результат.");
            }
        }
    }
}