public class FileReader {
    public TestData generateTestData(string path) {
        TestData data = new TestData();

        String[] lines = File.ReadAllLines(path);
        data.totalTestCases = Int32.Parse(lines[0]);

        if(data.totalTestCases > 100 || data.totalTestCases <= 0) throw new Exception("Outside boundary");

        for (var i = 1; i <= data.totalTestCases; i += 1) {
            int testCase = Int32.Parse(lines[i]);
            data.testCases.Add(testCase);
        }

        return data;
    }
}