// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests.InfiniLore.Server.Data;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CollectionOrderer : ITestCollectionOrderer {
    public const string CommandTestCollection = "CommandTestCollection";
    public const string QueryTestCollection = "QueryTestCollection";

    public IEnumerable<ITestCollection> OrderTestCollections(IEnumerable<ITestCollection> testCollections) {
        return testCollections.OrderBy(tc => tc.DisplayName switch {
            CommandTestCollection => 1,
            QueryTestCollection => 2,
            _ => 3
        });
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class TestClassPriorityAttribute(int priority) : Attribute {
    public int Priority { get; } = priority;
}

public class TestClassOrderer : ITestCaseOrderer {
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase => testCases.OrderBy(GetPriority);

    private static int GetPriority<TTestCase>(TTestCase testCase) where TTestCase : ITestCase {
        IAttributeInfo? attr = testCase.TestMethod.TestClass.Class.GetCustomAttributes(typeof(TestClassPriorityAttribute)).FirstOrDefault();
        return attr?.GetNamedArgument<int>("Priority") ?? 0;
    }
}
