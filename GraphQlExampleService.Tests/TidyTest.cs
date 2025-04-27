using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Sdk;

namespace GraphQlExampleService.Tests;

public class TidyTest
{
    private TidyNumberGenerator generator;
    private FileReader fileReader;

    private TestData testData;

    private const int MAX_NUMBER = 1000;

    private const string PATH = "/Users/dlassell/Documents/spacetraders/GraphQlExampleService.Tests/input-data.txt";

    // TODO: Add a Before All into this test class for the generator and file reader
    public TidyNumberGenerator GetGenerator() {
        if(generator == null) {
            generator = new TidyNumberGenerator();
            generator.generateTidyNumberList(MAX_NUMBER);
        }

        return generator;
    }

    public TestData ReadFile() {
        if(fileReader == null || testData == null) {
            fileReader = new FileReader();
            testData = fileReader.generateTestData(PATH);
        }

        return testData;
    }

    // Test Methods for finding Tidy Numbers
    [Fact]
    public async Task checkIfIsTidy()
    {
        TidyNumberGenerator generator = new TidyNumberGenerator();
        Assert.True(generator.isTidyNumber(8));
    }

    [Fact]
    public async Task checkIfNotTidy()
    {
        TidyNumberGenerator generator = new TidyNumberGenerator();
        Assert.False(generator.isTidyNumber(10));
    }

    // Test Methods for determining the previous tidy number
    [Fact]
    public async Task returnSelfWhenNumberIsTidy()
    {
        TidyNumberGenerator generator = new TidyNumberGenerator();
        Dictionary<int,int> tidyNumberList = generator.generateTidyNumberList(1000);
        int expectedPreviousTidyNumber = 1;

        int previousTidyNumber;
        tidyNumberList.TryGetValue(expectedPreviousTidyNumber, out previousTidyNumber);

        Assert.Equal(expectedPreviousTidyNumber, previousTidyNumber);
    }

    [Fact]
    public async Task returnPreviousTidyWhenNumberIsNotTidy()
    {
        TidyNumberGenerator generator = new TidyNumberGenerator();
        Dictionary<int,int> tidyNumberList = generator.generateTidyNumberList(1000);
        int expectedPreviousTidyNumber = 9;

        int previousTidyNumber;
        tidyNumberList.TryGetValue(10, out previousTidyNumber);

        Assert.Equal(expectedPreviousTidyNumber, previousTidyNumber);
    }

    // Test Methods for File Reader
    [Fact]
    public async Task fileDataMatchesFile()
    {
        FileReader fileReader = new FileReader();
        TestData data = fileReader.generateTestData(PATH);

        Assert.Equal(100, data.totalTestCases);
        Assert.Equal(data.totalTestCases, data.testCases.Count);
    }

        [Fact]
    public async Task throwErrorIfTooManyTestCases()
    {
        FileReader fileReader = new FileReader();
        Assert.Throws<Exception>(fileReader.generateTestData(PATH));
    }

    // Let's Output the file
    [Fact]
    public async Task actualAsk()
    {
        // Generate a list of prior tidy numbers
        TidyNumberGenerator generator = new TidyNumberGenerator();
        Dictionary<int,int> tidyNumberList = generator.generateTidyNumberList(1000);
        
        // Generate our test data from the file
        FileReader fileReader = new FileReader();
        TestData data = fileReader.generateTestData(PATH);

        // Format and Output the data
        for(int i = 0; i < data.totalTestCases; i++) {
            int testCase = data.testCases[i];
            int tidyNumber;

            tidyNumberList.TryGetValue(testCase, out tidyNumber);
            Console.WriteLine("Case #{0}: {1}", i + 1, tidyNumber);
        }
    }
}
