using System.Diagnostics.Metrics;

class Doctor
{
    public readonly string Id;
    public string Name;
    public string Departement;
    public readonly List<Patient> AssignedPatients = new List<Patient>();
    public readonly List<Doctor> Supervisees = new List<Doctor>();
    const string DefaultSupervisorId = "-";
    private static Dictionary<string, int> DepartementCounter = new Dictionary<string, int>();
    public string SupervisorId = DefaultSupervisorId;

    public Doctor(string name, string department)
    {
        Name = name;
        Departement = department;

        // idk where to get these values from, so hardcoding them in
        // its not clear where im supposed to get them from
        string prefix = department switch
        {
            "Cardiology" => "CAR",
            "Neurology" => "NEU",
            "Oncology" => "ONC",
            _ => "OTH"
        };

        if (!DepartementCounter.TryAdd(prefix, 1))
        {
            DepartementCounter[prefix]++;
        }

        string counterStr;


        if (DepartementCounter[prefix] < 10)
        {
            counterStr = $"{prefix}00{DepartementCounter[prefix]}";
        }
        else if (DepartementCounter[prefix] < 100)
        {
            counterStr = $"{prefix}0{DepartementCounter[prefix]}";
        }
        else
        {
            counterStr = $"{prefix}{DepartementCounter[prefix]}";
        }

        Id = counterStr;
    }
}