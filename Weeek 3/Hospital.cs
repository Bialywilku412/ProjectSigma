static class Hospital
{
    public const string Name = "Erasmus MC";
    public static readonly List<Doctor> Doctors = new List<Doctor>();
    public static readonly List<Patient> UnassignedPatients = new List<Patient>();
    public static readonly new string[] Departments = new string[3] { "Cardiology", "Neurology", "Oncology" };

    public static void AddDoctor(Doctor doctor)
    {
        if (!Doctors.Contains(doctor))
        {
            Doctors.Add(doctor);

            Console.WriteLine($"Doctor {doctor.Id} has been added");
        }
        else
        {
            Console.WriteLine($"Doctor {doctor.Id} already works in the hospital");
        }
    }

    public static void RemoveDoctor(string id)
    {
        foreach (var doctor in Doctors)
        {
            // finding this specific doctor
            if (id.Trim().ToUpper() == doctor.Id.Trim().ToUpper())
            {
                // moving patients from doctor before removing doc
                foreach (var patient in doctor.AssignedPatients)
                {
                    UnassignedPatients.Add(patient);
                }

                // removing this doc from being a supervisor from each other docs
                foreach (var doctorSupervisee in doctor.Supervisees)
                {
                    doctorSupervisee.SupervisorId = "-";
                }

                Doctors.Remove(doctor);

                Console.WriteLine($"Doctor {id} has been removed");

                return;
            }
        }

        Console.WriteLine($"Doctor with ID {id} not found");
    }

    public static void AddPatient(Patient patient)
    {
        if (!UnassignedPatients.Contains(patient))
        {
            UnassignedPatients.Add(patient);
            Console.WriteLine($"Patient {patient.Id} registered in the hospital");
        }
        else
        {
            foreach (var doctor in Doctors)
            {
                if (doctor.AssignedPatients.Contains(patient))
                {
                    Console.WriteLine($"Patient {patient.Id} is already assigned to doctor {doctor.Id}");
                    break;
                }
            }

            Console.WriteLine($"Patient {patient.Id} is already registered in the hospital");
        }
    }

    public static void RemovePatient(string id)
    {
        foreach (var patient in UnassignedPatients)
        {
            if (id == patient.Id)
            {
                UnassignedPatients.Remove(patient);

                Console.WriteLine($"Patient {id} has been removed");

                return;
            }
        }

        Console.WriteLine($"Patients with ID {id} not found");
    }
    
    public static void AssignDoctorToPatient(string doctorId, string patientId)
    {
        bool docExists = false;
        Doctor requiredDoc = null;
        Patient requiredPatient = null;

        foreach (var doctor in Doctors)
        {
            // already assigned to this doctor
            if (doctorId == doctor.Id)
            {
                foreach (var patient in doctor.AssignedPatients)
                {
                    if (patient.Id == patientId)
                    {
                        foreach (var d in Doctors)
                        {
                            if (doctorId == d.Id)
                            {
                                docExists = true;
                                requiredDoc = doctor;
                                break;
                            }
                        }
                        if (!docExists) { Console.WriteLine($"Doctor with ID {doctorId} not found"); }
                        Console.WriteLine($"Patient with ID {patientId} is already assigned to another doctor");

                        return;
                    }
                }
            }

            // already assigned to another doctor
            foreach (var patient in doctor.AssignedPatients)
            {
                if (patientId == patient.Id)
                {
                    foreach (var d in Doctors)
                    {
                        if (doctorId == d.Id)
                        {
                            docExists = true;
                            requiredDoc = doctor;
                            break;
                        }
                    }
                    if (!docExists) { Console.WriteLine($"Doctor with ID {doctorId} not found"); }
                    Console.WriteLine($"Patient with ID {patientId} is already assigned to another doctor");

                    return;
                }
            }
        }

        // does the patient exist
        // needs to be here because it only can be here after checking in Doctors
        bool patientExists = false;
        foreach (var patient in UnassignedPatients)
        {
            if (patientId == patient.Id)
            {
                requiredPatient = patient;
                patientExists = true;
                break;
            }
        }
        // does the doctor exist
        foreach (var doctor in Doctors)
        {
            if (doctorId == doctor.Id)
            {
                docExists = true;
                requiredDoc = doctor;
                break;
            }
        }

        // does patient exist in assigned to doctor
        foreach (var doc in Doctors)
        {
            foreach (var patient in doc.AssignedPatients)
            {
                if (patient.Id == patientId)
                {
                    patientExists = true;
                    break;
                }
            }
        }

        if (!docExists && !patientExists)
        {
            Console.WriteLine($"Doctor with ID {doctorId} not found");
            Console.WriteLine($"Patient with ID {patientId} not found");

            return;
        }
        if (!docExists)
        {
            Console.WriteLine($"Doctor with ID {doctorId} not found");

            return;
        }
        if (!patientExists)
        {
            Console.WriteLine($"Patient with ID {patientId} not found");

            return;
        }

        requiredDoc.AssignedPatients.Add(requiredPatient);
        UnassignedPatients.Remove(requiredPatient);

        Console.WriteLine($"Patient {patientId} is assigned to doctor {doctorId}");

    }

    public static  void AssignSuperviseeToSupervisor(string superviseeId, string supervisorId)
    {
        Doctor supervisorDoc = null;
        Doctor superviseeDoc = null;
        foreach(var doctor in Doctors)
        {
            if (doctor.Id == superviseeId)
            {
                superviseeDoc = doctor;
            }
            else if (doctor.Id == supervisorId)
            {
                supervisorDoc = doctor;
            }

            if (superviseeDoc != null && supervisorDoc != null)
            {
                break;
            }
        }

        // check for if the doctors exist and specific error message 
        if (superviseeDoc == null && supervisorDoc == null)
        {
            Console.WriteLine($"Doctor(s) not found:\n - {superviseeId}\n - {supervisorId}");
            return;
        }
        if (superviseeDoc == null)
        {
            Console.WriteLine($"Doctor(s) not found:\n - {superviseeId}");
            return;
        }
        if (supervisorDoc == null)
        {
            Console.WriteLine($"Doctor(s) not found:\n - {supervisorId}");
            return;
        }

        supervisorDoc.Supervisees.Add(superviseeDoc);
        superviseeDoc.SupervisorId = supervisorId;

        Console.WriteLine($"Added {superviseeId} to supervisor {supervisorId}");
    }
}
