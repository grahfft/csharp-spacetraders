public class TidyNumberGenerator {

    private Dictionary<int,int> previousTidyNumberMap;

    public Dictionary<int,int> generateTidyNumberList(int maxNumber) {
        if(previousTidyNumberMap == null) {
            previousTidyNumberMap = new Dictionary<int, int>();

            for(int i = 1; i <= maxNumber; i++) {
                if(isTidyNumber(i)) {
                    previousTidyNumberMap.Add(i,i);
                } else {
                    int previousTidyNumber;
                    previousTidyNumberMap.TryGetValue(i - 1, out previousTidyNumber);
                    previousTidyNumberMap.Add(i, previousTidyNumber);
                }
            }
        }

        return previousTidyNumberMap;
    }

    public bool isTidyNumber(int input) {
        int copy = input;
        int previousDigit = 10; // Choosing 10 since this is base Ten and we want to check the first item

        while(copy > 0) {
            int checkDigit = copy % 10;

            if (checkDigit > previousDigit) {
                return false;
            }
            previousDigit = checkDigit;
            copy = copy / 10;
        }

        return true;
    }
}