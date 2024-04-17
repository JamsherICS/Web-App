using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_Reservation_System
{
    class UserClass
    {
        static Train_Reservation_DBEntities TRDB = new Train_Reservation_DBEntities();
        static booked_ticket bt = new booked_ticket();
        static canceled_ticket ct = new canceled_ticket();
        static seat_availability sa = new seat_availability();
        static train_details td = new train_details();
        static user_details ud = new user_details();
        static int uid;


        public static void userFunction()
        {
            Console.Write("\n  New User -> 1\n  Old User -> 2\n  Exit     -> 0\n");
            Console.WriteLine();
            Console.Write("Enter Your Choice: ");
            int input;

            while (!int.TryParse(Console.ReadLine(), out input) || (input < 0 || input > 2))
            {
                Console.WriteLine("Please enter a valid option (1, 2, or 0).");
                Console.Write("Enter your choice: ");
            }

            switch (input)
            {
                case 0:
                    Console.WriteLine("Exiting the program...");
                    return; // Exit the program
                case 1:
                    Console.WriteLine();
                    Console.WriteLine(".................New User selected.................");
                    Console.WriteLine();
                    // new user create account
                    userDetailsFun();
                    Console.WriteLine();
                    userPartOptions();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine(".................Old User selected.................");
                    Console.WriteLine();
                    // old user login
                    oldUserLogin();
                    Console.WriteLine();
                    
                    
                    break;
            }
        }

        //method for user registration
        static void userDetailsFun()
        {
            try
            {
                Console.Write("Enter new user id: ");
                uid = Convert.ToInt32(Console.ReadLine());

                // Check if the user ID already exists
                var checkUID = TRDB.user_details.SingleOrDefault(ud => ud.userId == uid);
                if (checkUID != null)
                {
                    Console.WriteLine("User ID already exists.");
                    userDetailsFun(); 
                    return;
                }

                ud.userId = uid;
                Console.Write("Enter Name : ");
                ud.userName = Console.ReadLine();
                Console.Write("Enter Age : ");
                ud.age = int.Parse(Console.ReadLine());
                Console.Write("Enter Passcode : ");
                ud.passcode = Console.ReadLine();

                Console.Write("Enter your WhatsApp number: ");                                          
                ud.mobile_number= Console.ReadLine();

                TRDB.user_details.Add(ud);
                TRDB.SaveChanges();
                Console.Clear();
                Console.WriteLine("Successfully Registered!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric user ID and age.");
                userDetailsFun(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                
            }
        }


        static void userPartOptions()
        {
            Console.WriteLine();
            Console.WriteLine("------USER DASHBOARD------");
            Console.Write("\n  Book Ticket        ->    1\n  Booked Ticket      ->    2\n  Cancel Ticket      ->    3\n  Cancelled Tickets  ->    4\n  Log Out            ->    5\n  Exit               ->    6\n");
            Console.WriteLine();
            Console.Write("Enter Your Choice : ");
            int input;

            while (!int.TryParse(Console.ReadLine(), out input) || (input < 1 || input > 6))
            {
                Console.WriteLine("Please enter a valid option (1, 2, 3 or 4).");
                Console.Write("Enter your choice: ");
            }

            switch (input)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".................Book Ticket Selected.................");
                    Console.WriteLine();
                    ShowAllTrains();
                    BookTicket(uid);
                    userPartOptions();
                    break; // Exit the program
                case 2:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".................Show Booked Ticket Selected.................");
                    Console.WriteLine();
                    ShowBookedTickets(uid);                    
                    userPartOptions();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".................Cancel Ticket Selected.................");
                    Console.WriteLine();
                    CancelTicket();
                    userPartOptions();
                    break;

                case 4:
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine(".................Show Cancelled Ticket Selected.................");
                    Console.WriteLine();
                    ShowBookingCancellation(uid);
                    userPartOptions();
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

        //Existing user login
        static void oldUserLogin()
        {
            try
            {
                Console.Write("Enter User ID: ");
                uid = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                var user = TRDB.user_details.FirstOrDefault(ud => ud.userId == uid && ud.passcode == password);

                if (user != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Welcome, {user.userName}!");
                    userPartOptions();
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                    oldUserLogin();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric user ID.");
                oldUserLogin();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                // You can handle specific exceptions here based on your requirements
            }
        }

        //Book Ticket
        static void BookTicket(int uid)
        {
            Console.WriteLine();
            Console.Write("Enter train number: ");
            int trainNo = Convert.ToInt32(Console.ReadLine());

            // Check if the train is active
            var train = TRDB.train_details.FirstOrDefault(td => td.trainNo == trainNo && td.Status == "Active");
            if (train == null)
            {
                Console.WriteLine("Sorry, this train is not active.");
                return;
            }

            Console.Write("Enter passenger name: ");
            string passengerName = Console.ReadLine();

            Console.Write("Enter passenger age: ");
            int passengerAge = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter ticket class (1AC, 2AC, 3AC, SL): ");
            string ticketClass = Console.ReadLine().ToUpper();           


            // Check seat availability
            var seatAvailability = TRDB.seat_availability.FirstOrDefault(sa => sa.trainNo == trainNo);
            if (seatAvailability == null || !HasAvailableSeats(seatAvailability, ticketClass))
            {
                Console.WriteLine("Sorry, there are no available seats in the selected class.");
                return;
            }

            // Calculate total fare
            var train2 = TRDB.train_classes.FirstOrDefault(tc => tc.trainNo == trainNo && tc.train_details.Status == "Active");
            float fare = GetFare(train2, ticketClass);

            // Deduct booked seats from seat availability
            UpdateSeatAvailability(seatAvailability, ticketClass);



            // Add booked ticket to the database
            var newTicket = new booked_ticket
            {
                PNR = GenerateUniquePNR(),
                userId = uid,
                trainNo = trainNo,
                passengerName = passengerName,
                passengerAge = passengerAge,
                ticketClass = ticketClass,
                totalFare = fare,
                bookingDateTime = DateTime.Now
            };

            TRDB.booked_ticket.Add(newTicket);
            TRDB.SaveChanges();            

            Console.WriteLine("Ticket booked successfully.");
        }

        static bool IsPNRUnique(int pnr)
        {
            // Check if the generated PNR already exists in the database
            return TRDB.booked_ticket.Any(bt => bt.PNR == pnr);
        }

        static Random random = new Random();
        static int GenerateUniquePNR()
        {
            int pnr;
            do
            {
                // Generate a random number for PNR
                pnr = random.Next(1000, 5001); // Range from 1000 to 5000
            } while (IsPNRUnique(pnr)); // Check if the generated PNR is unique
            return pnr;
        }



        static void CancelTicket()
        {
            
            int userId = uid;
            var ticket = TRDB.booked_ticket.FirstOrDefault(bt => bt.userId == userId);
            if (ticket == null)
            {
                Console.WriteLine("No booked ticket for cancel, Please book the tickets");
                userPartOptions();
                return;
            }

            ShowBookedTickets(uid);

            Console.Write("Enter PNR number:");
            int pnr = Convert.ToInt32(Console.ReadLine());

            // Find the booked ticket
            //var ticket = TRDB.booked_ticket.FirstOrDefault(bt => bt.PNR == pnr && bt.userId == userId);
            if (ticket == null)
            {
                Console.WriteLine("No ticket found with the specified PNR number.");
                return;
            }

            // Refund the amount
            Console.WriteLine();
            double refundAmount = ticket.totalFare * 0.75f; // Assuming 75% refund policy
            Console.WriteLine($"Refund amount: {refundAmount}");

            // Update seat availability
            var seatAvailability = TRDB.seat_availability.FirstOrDefault(sa => sa.trainNo == ticket.trainNo);
            if (seatAvailability != null)
            {
                AddCancelledSeats(seatAvailability, ticket.ticketClass);
            }

            // Add cancelled ticket to the database
            var cancelledTicket = new canceled_ticket
            {
                canceledId = pnr, // Assuming canceledId is PNR
                userId = userId,
                trainNo = ticket.trainNo,
                cancellationDateTime = DateTime.Now,
                refundAmount = refundAmount
            };

            TRDB.canceled_ticket.Add(cancelledTicket);
            TRDB.SaveChanges();

            // Remove booked ticket from the database
            TRDB.booked_ticket.Remove(ticket);
            TRDB.SaveChanges();

            Console.WriteLine("Ticket cancelled successfully.");
        }

        //method for showing all trains
        static void ShowAllTrains()
        {
            var trains = TRDB.train_details.Where(td => !td.isDeleted).ToList();
            Console.WriteLine("All Trains:");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Train No | Name            | From      | To        | Timing                  | Status      | 1AC          | 2AC          | 3AC          | Sleeper     |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------");
            foreach (var train in trains)
            {
                var saa = TRDB.seat_availability.FirstOrDefault(sa => sa.trainNo == train.trainNo);
                // Fetch train classes for the current train
                var tcc = TRDB.train_classes.FirstOrDefault(tc => tc.trainNo == train.trainNo);

                Console.WriteLine($"| {train.trainNo,-8} | {train.trainName,-15} | {train.From,-9} | {train.To,-9} | {train.FromTiming} ----> {train.ToTiming,-5} | {train.Status,-11} | {saa.C1AC,-3} : {tcc.C1AC}rs | {saa.C2AC,-3} : {tcc.C2AC}rs | {saa.C3AC,-3} : {tcc.C3AC}rs | {saa.SL,-3} : {tcc.SL}rs |");
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------------------------------------------");
        }




        //method for show booked tickets
        static void ShowBookedTickets(int userId)
        {
            var bookedTickets = TRDB.booked_ticket.Where(bt => bt.userId == userId).ToList();
            

            if (bookedTickets.Count == 0)
            {
                Console.WriteLine("No booked tickets found for the specified user.");
                return;
            }

            Console.WriteLine("Booked Tickets:");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| PNR    | Train No  | Train Name       | Passenger Name | From      | To        | Timing                  | Class  | Fare   | Booking Date          |");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------");

            foreach (var ticket in bookedTickets)
            {
                var train = TRDB.train_details.FirstOrDefault(sa => sa.trainNo == ticket.trainNo);
                
                Console.WriteLine($"| {ticket.PNR,-6} | {ticket.trainNo,-9} | {train.trainName,-15} | {ticket.passengerName,-14} | {train.From,-9} | {train.To,-9} | {train.FromTiming} ----> {train.ToTiming,-5} | {ticket.ticketClass,-6} | {ticket.totalFare,-6} | {ticket.bookingDateTime,-21} |");
            }

            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------");
        }



        //method for show all tickets Booked and Cancelled in one place
        static void ShowBookingCancellation(int uid)
        {
            int userId = uid;
            var cancelledTickets = TRDB.canceled_ticket.Where(t => t.userId == userId).ToList();

            if (cancelledTickets.Count == 0)
            {
                Console.WriteLine("No cancelled tickets found for the specified user.");
                return;
            }

            Console.WriteLine("Cancelled Tickets:");
            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("| Cancelled ID  | PNR   | Train No  | Cancellation Date       | Refund Amount |");
            Console.WriteLine("-------------------------------------------------------------------------------");

            foreach (var ticket in cancelledTickets)
            {
                Console.WriteLine($"| {ticket.canceledId,-13} | {ticket.canceledId,-5} | {ticket.trainNo,-9} | {ticket.cancellationDateTime,-23} | {ticket.refundAmount,-13} |");
            }

            Console.WriteLine("-------------------------------------------------------------------------------");
        }


        static bool HasAvailableSeats(seat_availability seatAvailability, string ticketClass)
        {
            switch (ticketClass)
            {
                case "1AC":
                    return seatAvailability.C1AC > 0;
                case "2AC":
                    return seatAvailability.C2AC > 0;
                case "3AC":
                    return seatAvailability.C3AC > 0;
                case "SL":
                    return seatAvailability.SL > 0;
                default:
                    return false;
            }
        }

        static float GetFare(train_classes train, string ticketClass)
        {
            switch (ticketClass)
            {
                case "1AC":
                    return (float)train.C1AC;
                case "2AC":
                    return (float)train.C2AC;
                case "3AC":
                    return (float)train.C3AC;
                case "SL":
                    return (float)train.SL;
                default:
                    return 0;
            }
        }

        static void UpdateSeatAvailability(seat_availability seatAvailability, string ticketClass)
        {
            switch (ticketClass)
            {
                case "1AC":
                    seatAvailability.C1AC--;
                    break;
                case "2AC":
                    seatAvailability.C2AC--;
                    break;
                case "3AC":
                    seatAvailability.C3AC--;
                    break;
                case "SL":
                    seatAvailability.SL--;
                    break;
            }
        }

        static void AddCancelledSeats(seat_availability seatAvailability, string ticketClass)
        {
            switch (ticketClass)
            {
                case "1AC":
                    seatAvailability.C1AC++;
                    break;
                case "2AC":
                    seatAvailability.C2AC++;
                    break;
                case "3AC":
                    seatAvailability.C3AC++;
                    break;
                case "SL":
                    seatAvailability.SL++;
                    break;
            }
        }

    }
}
