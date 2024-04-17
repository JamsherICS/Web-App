using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_Reservation_System
{
    class AdminClass
    {
        static Train_Reservation_DBEntities TRDB = new Train_Reservation_DBEntities();
        static train_details td = new train_details();
        static admin_details ad = new admin_details();
        static train_classes tc = new train_classes();
        static int uid;


        public static void adminFunction()
        {
            Console.Write("Enter Admin id: ");
            uid = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var admin = TRDB.admin_details.FirstOrDefault(ad => ad.adminId.Equals(uid) && ad.passcode.Equals(password));

            if (admin != null)
            {
                Console.Clear();
                Console.Write("Admin login successfully...");
                Console.WriteLine();
                adminPartOptions();
            }
            else
            {
                Console.WriteLine("Invalid login credentials!");
                adminFunction();
            }
        }



       static void adminPartOptions()
        {
            Console.WriteLine();
            Console.WriteLine("------ADMIN DASHBOARD------");
            

            Console.Write("\n  Add Train         ->    1\n  Modify Train      ->    2\n  Soft Delete       ->    3\n  Show All Train    ->    4\n  Log Out           ->    5\n  Exit              ->    6\n");
            Console.WriteLine();
            Console.Write("Enter Your Choice : ");
            
            int input;

            while (!int.TryParse(Console.ReadLine(), out input) || (input < 1 || input > 6))
            {
                
                Console.WriteLine("Please enter a valid option (1 to 6).");
                Console.Write("Enter your choice: ");
                
            }

            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Add Train Option Selected.............");
                    Console.WriteLine();
                    AddTrain();                    
                    adminPartOptions();
                    break; 
                case 2:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Modify Train Option Selected.............");
                    Console.WriteLine();
                    modifyTrain();                   
                    adminPartOptions();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Soft Delete Option Selected.............");
                    Console.WriteLine();
                    SoftDeleteTrain();                    
                    adminPartOptions();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine(".............Show All Train Option Selected.............");
                    Console.WriteLine();
                    Console.Clear();
                    ShowAllTrains();                    
                    adminPartOptions();
                    break;
                
                case 5:
                    Console.Clear();
                    Program.mainFunction();
                    Console.WriteLine();
                    break;
                case 6:
                    Console.WriteLine("Exiting the program...");
                    return;

            }
        }

        static void AddTrain()
        {
            Console.Write("Enter new unique train number: ");
            int trainNo = Convert.ToInt32(Console.ReadLine());

            // Check if the train number already exists
            var existingTrain = TRDB.train_details.FirstOrDefault(td => td.trainNo == trainNo);
            if (existingTrain != null)
            {
                Console.WriteLine("Train with the same number already exists.");
                AddTrain();
                return;
            }

            Console.Write("Enter train name: ");
            string trainName = Console.ReadLine();

            Console.Write("Enter origin: ");
            string from = Console.ReadLine();

            Console.Write("Enter destination: ");
            string to = Console.ReadLine();

            Console.Write("Enter Departure Time(ex-09:30 AM): ");
            string departTime = Console.ReadLine(); 
            
            Console.Write("Enter Arrival Time((ex-09:30 PM)): ");
            string arrivTime = Console.ReadLine();

            Console.WriteLine();
            //adding classes(seat fares) and seat availability

            Console.WriteLine("Enter total number of each class seats");
            Console.Write("1AC seats: ");
            int firstSeat = Convert.ToInt32(Console.ReadLine());
            Console.Write("2AC seats: ");
            int secondSeat = Convert.ToInt32(Console.ReadLine());
            Console.Write("3AC seats: ");
            int thirdSeat = Convert.ToInt32(Console.ReadLine());
            Console.Write("SL seats: ");
            int SLSeat = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Console.WriteLine("Enter price/fare for each classes");
            Console.Write("1AC price: ");
            int firstPrice = Convert.ToInt32(Console.ReadLine());
            Console.Write("2AC price: ");
            int secondPrice = Convert.ToInt32(Console.ReadLine());
            Console.Write("3AC price: ");
            int thirdPrice = Convert.ToInt32(Console.ReadLine());
            Console.Write("SL price: ");
            int SLPrice = Convert.ToInt32(Console.ReadLine());


            // Create a new train object
            var newTrain = new train_details
            {
                trainNo = trainNo,
                trainName = trainName,
                From = from,
                To = to,
                Status = "Active", // newly added trains are active by default
                FromTiming = departTime,
                ToTiming=arrivTime
            };


            var seatAvial = new seat_availability 
            {
                trainNo=trainNo,
                C1AC=firstSeat,
                C2AC=secondSeat,
                C3AC=thirdSeat,
                SL=SLSeat
            };

            var classesFares = new train_classes
            {
                trainNo = trainNo,
                C1AC = firstPrice,
                C2AC = secondPrice,
                C3AC = thirdPrice,
                SL = SLPrice
            };

            // Add the new train to the database
            TRDB.train_details.Add(newTrain);
            TRDB.seat_availability.Add(seatAvial);
            TRDB.train_classes.Add(classesFares);

            TRDB.SaveChanges();
            Console.WriteLine();
            Console.WriteLine("Train added successfully.");
        }

        
        static void ShowAllTrains()
        {
            var trains = TRDB.train_details.ToList();
            Console.WriteLine("All Trains:");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Train No | Name            | From      | To        | Timing                  | Status      | 1AC          | 2AC          | 3AC          | Sleeper     | isDeleted |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var train in trains)
            {
                var saa = TRDB.seat_availability.FirstOrDefault(sa => sa.trainNo == train.trainNo);
                // Fetch train classes for the current train
                var tcc = TRDB.train_classes.FirstOrDefault(tc => tc.trainNo == train.trainNo);

                Console.WriteLine($"| {train.trainNo,-8} | {train.trainName,-15} | {train.From,-9} | {train.To,-9} | {train.FromTiming} ----> {train.ToTiming,-5} | {train.Status,-11} | {saa.C1AC,-3} : {tcc.C1AC}rs | {saa.C2AC,-3} : {tcc.C2AC}rs | {saa.C3AC,-3} : {tcc.C3AC}rs | {saa.SL,-3} : {tcc.SL}rs | {train.isDeleted,-9} |");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }


        static void modifyTrain()
        {
            ShowAllTrains();
            Console.WriteLine();
            Console.Write("Enter train number for modification: ");
            int trainNumber = Convert.ToInt32(Console.ReadLine());
            var modifiedTrain = TRDB.train_details.FirstOrDefault(td => td.trainNo == trainNumber);

            if (modifiedTrain == null)
            {
                Console.WriteLine("Train not exists, Please try again!");
                Console.WriteLine();
                modifyTrain();
            }

            start:
            Console.WriteLine("\n  Change Train Name      ->     1\n  Change Origin          ->     2\n  Change Destination     ->     3\n  Activate Train         ->     4\n  Deactivate Train       ->     5\n  Restore Train          ->     6\n  Back                   ->     7\n");
            Console.Write($"Enter your choice for {trainNumber}: ");
            int choice;

            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 7))
            {
                Console.WriteLine("Please enter a valid option (1 to 7).");
                Console.Write("Enter your choice: ");               
            }


            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Change Train Name Selected.............");
                    Console.WriteLine();
                    Console.Write("Enter new Name for Train: ");
                    string name = Console.ReadLine();

                    while (name == modifiedTrain.trainName)
                    {
                        Console.Write("You entered same name. \nPlease enter new one: ");
                        name = Console.ReadLine();
                    }

                    modifiedTrain.trainName = name;
                    TRDB.SaveChanges();
                    Console.WriteLine();
                    ShowAllTrains();
                    goto start;

                case 2:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Change Train Origin Selected.............");
                    Console.WriteLine();
                    Console.Write("Enter new Origin for Train: ");
                    string origin = Console.ReadLine();
                    while (origin == modifiedTrain.From)
                    {
                        Console.Write("You entered same origin. \nPlease enter new one: ");
                        origin = Console.ReadLine();
                    }
                    modifiedTrain.From = origin;
                    TRDB.SaveChanges();
                    Console.WriteLine();
                    ShowAllTrains();
                    goto start;

                case 3:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Change Train Destination Selected.............");
                    Console.WriteLine();
                    Console.Write("Enter new Destination for Train: ");
                    string destination = Console.ReadLine();
                    while (destination == modifiedTrain.To)
                    {
                        Console.Write("You entered same destination. \nPlease enter new one: ");
                        destination = Console.ReadLine();
                    }
                    modifiedTrain.To = destination;
                    TRDB.SaveChanges();
                    Console.WriteLine();
                    ShowAllTrains();
                    goto start;

                case 4:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Activate Train Selected.............");
                    Console.WriteLine();    
                    activateTrain(trainNumber);
                    Console.WriteLine();
                    ShowAllTrains();
                    goto start;

                case 5:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Deactivate Train Selected.............");
                    Console.WriteLine();
                    deactivateTrain(trainNumber);
                    Console.WriteLine();
                    ShowAllTrains();
                    goto start;

                case 6:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".............Restore Train Selected.............");
                    Console.WriteLine();
                    restoreTrain(trainNumber);
                    Console.WriteLine();
                    ShowAllTrains();
                    goto start;

                case 7:
                    Console.Clear();           
                    adminPartOptions();
                    return;

            }
        }

        //method for soft delete
        static void SoftDeleteTrain()
        {
            ShowAllTrains();
            Console.WriteLine();
            Console.Write("Enter train number to soft delete train: ");
            int trainNumber = Convert.ToInt32(Console.ReadLine());
            // Find the train by its train number
            var train = TRDB.train_details.FirstOrDefault(td => td.trainNo == trainNumber);

            // If the train exists, mark it as deleted
            if (train != null)
            {
                if (train.isDeleted == true)
                {
                    Console.WriteLine("Already soft deleted.");
                }
                else
                {
                    train.isDeleted = true;

                    // Save changes to persist the soft delete
                    TRDB.SaveChanges();

                    Console.WriteLine("Train successfully soft deleted.");
                    Console.Clear();
                    ShowAllTrains();
                }
                
            }
            else
            {
                Console.WriteLine("Train not found. No changes were made.");
            }
        }


      
        //method for activating the train
        static void activateTrain(int trainNumber)
        {
            var activateTrain = TRDB.train_details.SingleOrDefault(td => td.trainNo == trainNumber);
            if (activateTrain.Status!="Active")
            {
                activateTrain.Status = "Active";
                TRDB.SaveChanges();
                Console.WriteLine("Train Successfully Activated...");                
            }
            else
            {
                Console.WriteLine("Already Active, Please Deactivate first");                
            }
        }

        //method for deactivating the train
        static void deactivateTrain(int trainNumber)
        {
            
            var deactTrain = TRDB.train_details.SingleOrDefault(td => td.trainNo == trainNumber);
            if (deactTrain.Status != "Deactivated")
            {
                deactTrain.Status = "Deactivated";
                TRDB.SaveChanges();
                Console.WriteLine("Train Successfully Deactivated...");
            }
            else
            {
                Console.WriteLine("Already Deactivated, Please Activate first");
            }
        }

        //method for Restore train
        static void restoreTrain(int trainNumber)
        {

            var restoreTrain = TRDB.train_details.SingleOrDefault(td => td.trainNo == trainNumber);
            if (restoreTrain.isDeleted != false)
            {
                restoreTrain.isDeleted = false;
                TRDB.SaveChanges();
                Console.WriteLine("Train Successfully Restored(isDeleted->False)...");
            }
            else
            {
                Console.WriteLine("Already Restored(), Please Delete(make isDelete->True) first");
            }
        }
    }
}
