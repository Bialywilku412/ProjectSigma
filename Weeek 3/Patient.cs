class Patient
{
    public readonly string Id;
    public string Name;
    public int Age;
    private static int counter = 1;

    public Patient(string name, int age)
    {
        Name = name;
        Age = age;
        string counterStr;

        if (counter < 10)
        {
            counterStr = $"PAT00{counter}";
        }
        else if (counter < 100)
        {
            counterStr = $"PAT0{counter}";
        }
        else
        {
            counterStr = $"PAT{counter}";
        }

        Id = counterStr;
        counter++;
    }
}
