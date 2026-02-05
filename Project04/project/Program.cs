using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Dynamic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelSystem
{ 
    // HotelSystem class: Manages all entities in the system
    [Serializable]
    class HotelSystem
    {
        public List<Room> Rooms { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Service> Services { get; set; }
        public List<Guest> Guests { get; set; }
        public List<Manager> Managers { get; set; }
        public List<Payment> Payments { get; set; }

        public HotelSystem()
        {
            Rooms = new List<Room>();
            Reservations = new List<Reservation>();
            Services = new List<Service>();
            Guests = new List<Guest>();
            Managers = new List<Manager>();
            Payments = new List<Payment>();
        }


        // دوال التخزين في الملفات
        public static void StoreRoom(Room room)
        {
            FileStream fileStream = new FileStream("Rooms.txt", FileMode.Append);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, room);
            fileStream.Close();
        }

        public static void StoreReservation(Reservation reservation)
        {
            FileStream fileStream = new FileStream("Reservations.txt", FileMode.Append);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, reservation);
            fileStream.Close();
        }

        public static void StoreGuest(Guest guest)
        {
            FileStream fileStream = new FileStream("Guests.txt", FileMode.Append);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, guest);
            fileStream.Close();
        }

        public static void StoreService(Service service)
        {
            FileStream fileStream = new FileStream("Services.txt", FileMode.Append);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, service);
            fileStream.Close();
        }
        public static void StoreManager(Manager manager)
        {
            FileStream fileStream = new FileStream("Manager.txt", FileMode.Append);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, manager);
            fileStream.Close();
        }

        public static void StorePayment(Payment payment)
        {
            FileStream fileStream = new FileStream("Payments.txt", FileMode.Append);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fileStream, payment);
            fileStream.Close();
        }

        // دوال استرجاع البيانات من الملفات 
        public static List<Room> ReturnRoomList()
        {
            try
            {
                FileStream fileStream = new FileStream("Rooms.txt", FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                List<Room> list = new List<Room>();
                while (fileStream.Position < fileStream.Length)
                {
                    list.Add((Room)formatter.Deserialize(fileStream));
                }
                fileStream.Close();
                return list;
            }
            catch (Exception ex)    
            {
                Console.WriteLine("Error while reading the room list: " + ex.Message);
                return new List<Room>();
            }
        }

        public static List<Reservation> ReturnReservationList()
        {
            try
            {
                FileStream fileStream = new FileStream("Reservations.txt", FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                List<Reservation> list = new List<Reservation>();
                while (fileStream.Position < fileStream.Length)
                {
                    list.Add((Reservation)formatter.Deserialize(fileStream));
                }
                fileStream.Close();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading the reservation list: " + ex.Message);
                return new List<Reservation>();
            }
        }

        public static List<Guest> ReturnGuestList()
        {
            try
            {
                FileStream fileStream = new FileStream("Guests.txt", FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                List<Guest> list = new List<Guest>();
                while (fileStream.Position < fileStream.Length)
                {
                    list.Add((Guest)formatter.Deserialize(fileStream));
                }
                fileStream.Close();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading the guest list: " + ex.Message);
                return new List<Guest>();
            }
        }

        public static List<Service> ReturnServiceList()
        {
            try
            {
                FileStream fileStream = new FileStream("Services.txt", FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                List<Service> list = new List<Service>();
                while (fileStream.Position < fileStream.Length)
                {
                    list.Add((Service)formatter.Deserialize(fileStream));
                }
                fileStream.Close();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading the service list: " + ex.Message);
                return new List<Service>();
            }
        }

        public static List<Payment> ReturnPaymentList()
        {
            try
            {
                FileStream fileStream = new FileStream("Payments.txt", FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                List<Payment> list = new List<Payment>();
                while (fileStream.Position < fileStream.Length)
                {
                    list.Add((Payment)formatter.Deserialize(fileStream));
                }
                fileStream.Close();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading the payment list: " + ex.Message);
                return new List<Payment>();
            }
        }
        // دوال عرض البيانات المخزنة من الملفات
        public static void DisplayAllRooms()
        {
            if (File.Exists("Rooms.txt"))
            {
                List<Room> roomList = ReturnRoomList();
                foreach (Room room in roomList)
                {
                    Console.WriteLine("Room Number: " + room.Number + ", Type: " + room.Type + ", Price: " + room.BasePrice + ", Available: " + room.IsAvailable);
                }
            }
            else
            {
                Console.WriteLine("No Rooms Found.");
            }
        }

        public static void DisplayAllReservations()
        {
            if (File.Exists("Reservations.txt"))
            {
                List<Reservation> reservationList = ReturnReservationList();
                foreach (Reservation reservation in reservationList)
                {
                    Console.WriteLine("Reservation ID: " + reservation.ID + ", Room: " + reservation.RoomNumber + ", Guest: " + reservation.GuestNationalID + ", Status: " + reservation.Status);
                }
            }
            else
            {
                Console.WriteLine("No Reservations Found.");
            }
        }

        public static void DisplayAllGuests()
        {
            if (File.Exists("Guests.txt"))
            {
                List<Guest> guestList = ReturnGuestList();
                foreach (Guest guest in guestList)
                {
                    Console.WriteLine("Name: " + guest.Name + ", National ID: " + guest.NationalID + ", Phone: " + guest.PhoneNo + ", Balance: " + guest.BankBalance);
                }
            }
            else
            {
                Console.WriteLine("No Guests Found.");
            }
        }

        public static void DisplayAllServices()
        {
            if (File.Exists("Services.txt"))
            {
                List<Service> serviceList = ReturnServiceList();
                foreach (Service service in serviceList)
                {
                    Console.WriteLine("Service ID: " + service.ID + ", Description: " + service.Description + ", Cost: " + service.Cost);
                }
            }
            else
            {
                Console.WriteLine("No Services Found.");
            }
        }

        public static void DisplayAllPayments()
        {
            if (File.Exists("Payments.txt"))
            {
                List<Payment> paymentList = ReturnPaymentList();
                foreach (Payment payment in paymentList)
                {
                    Console.WriteLine("Bill Number: " + payment.BillNumber + ", Amount: " + payment.Amount + ", Status: " + payment.Status);
                }
            }
            else
            {
                Console.WriteLine("No Payments Found.");
            }
        }
    }

    // Base class representing a user in the system
    [Serializable]
    abstract class User
    {
        public string ID { get; set; }
        public string Password { get; set; }

        public User(string id, string password)
        {
            ID = id;
            Password = password;
        }

        public bool Login(string id, string password)
        {
            if (ID == id && Password == password)
            {
                Console.WriteLine("Login successful. Welcome, User ID: " + ID);

                if (this is Guest)
                {
                    Console.WriteLine("Guest Login Successful!");
                    DisplayOptionsMenuForGuest();
                }
                else if (this is Manager)
                {
                    Console.WriteLine("Manager Login Successful!");
                    DisplayOptionsMenuForManager();
                }

                return true;
            }
            else
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                return false;
            }
        }

        public void Logout()
        {
            Console.WriteLine("User logged out. Goodbye, User ID: " + ID);
        }
        public static void DisplayLoginScreen()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Welcome to the Hotel Management System");
            Console.WriteLine("======================================");
            
            bool x = true;
            while (x)
            {
                Console.WriteLine("\nPlease log in to continue:");
                Console.WriteLine("1 - Manager");
                Console.WriteLine("2 - Guest");
                Console.WriteLine("0 - Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nManager Login:");
                        Console.WriteLine("Enter Manager ID: ");
                        string managerId = Console.ReadLine();
                        Console.WriteLine("Enter Password: ");
                        string managerPassword = Console.ReadLine();

                        Manager manager = new Manager(managerId, managerPassword);
                        if (manager.Login("m2024", "00"))
                        {
                            return;
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nGuest Login:");
                        Console.Write("Enter Guest ID: ");
                        string guestId = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string guestPassword = Console.ReadLine();

                        Guest verifiedGuest = HotelSystem.ReturnGuestList().Find(g => g.NationalID == guestId && g.Password == guestPassword);

                        if (verifiedGuest != null)
                        {
                            Console.WriteLine("Guest Login Successful!");
                            verifiedGuest.DisplayOptionsMenuForGuest();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Guest ID or Password. Please try again.");
                        }
                        break;

                    case "0":
                        Console.WriteLine("Exiting the system. Goodbye!");
                        x = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void DisplayOptionsMenuForGuest()
        {
            bool x = true;
            while (x)
            {
                Guest g = HotelSystem.ReturnGuestList().Find(guest => guest.NationalID == this.ID);
                Manager m = new Manager("m2024", "00");
                if (g == null)
                {
                    Console.WriteLine("Guest not found or invalid. Please log in again.");
                    return; // Exit the method if the guest is not valid
                }

                //Console.WriteLine("Welcome, " + g.Name + "!");

                Console.WriteLine("Welcome, " + g.Name + "!");
                Console.WriteLine("\nGuest Options Menu:");
                Console.WriteLine("1. Reserve a Room");
                Console.WriteLine("2. Check In");
                Console.WriteLine("3. Request a Service");
                Console.WriteLine("4. Check Out");
                Console.WriteLine("5. Pay for a Reservation");
                Console.WriteLine("6. Pay for a Service");
                Console.WriteLine("7. Logout");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        m.ViewAllRooms(HotelSystem.ReturnRoomList());
                        Console.Write("Enter room number to reserve: ");
                        string roomNumber = Console.ReadLine();
                        Console.Write("Enter check-in date (YYYY-MM-DD): ");
                        string checkInDate = Console.ReadLine();
                        Console.Write("Enter check-out date (YYYY-MM-DD): ");
                        string checkOutDate = Console.ReadLine();

                        Room roomToReserve = HotelSystem.ReturnRoomList().Find(r => r.Number == roomNumber);
                        if (roomToReserve != null)
                        {
                            g.ReserveRoom(roomToReserve, checkInDate, checkOutDate);
                        }
                        else
                        {
                            Console.WriteLine("Room not found or not available.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter reservation ID to check in: ");
                        string reservationIDCheckIn = Console.ReadLine();
                        Reservation reservationToCheckIn = HotelSystem.ReturnReservationList().Find(r => r.ID == reservationIDCheckIn);
                        if (reservationToCheckIn != null)
                        {
                            g.CheckIn(reservationToCheckIn);
                        }
                        else
                        {
                            Console.WriteLine("Reservation not found.");
                        }
                        break;
                    case "3":
                        Console.Write("Create Service ID: ");
                        string reservationIDService = Console.ReadLine();
                        Console.Write("Write service description: -car rental- or -kids zone- ");
                        string serviceDescription = Console.ReadLine();
                        Console.Write("Enter number of children or number of rental days: ");
                        int count = int.Parse(Console.ReadLine());

                        Reservation reservationForService = HotelSystem.ReturnReservationList().Find(r => r.ID == reservationIDService);
                        if (reservationForService != null)
                        {
                            Service service = new Service(g.NationalID, reservationIDService, serviceDescription, count);
                            g.RequestService(service, reservationForService, HotelSystem.ReturnServiceList());
                        }
                        else
                        {
                            Console.WriteLine("Reservation not found.");
                        }
                        break;
                    case "4":
                        Console.Write("Enter reservation ID to check out: ");
                        string reservationIDCheckOut = Console.ReadLine();
                        Reservation reservationToCheckOut = HotelSystem.ReturnReservationList().Find(r => r.ID == reservationIDCheckOut);
                        if (reservationToCheckOut != null)
                        {
                            g.CheckOut(reservationToCheckOut);
                        }
                        else
                        {
                            Console.WriteLine("Reservation not found.");
                        }
                        break;
                    case "5":
                        Console.Write("Enter reservation ID to pay for: ");
                        string reservationIDPay = Console.ReadLine();

                        Reservation reservationToPay = HotelSystem.ReturnReservationList().Find(r => r.ID == reservationIDPay);
                        if (reservationToPay != null)
                        {
                            g.PayForReservation(reservationToPay);
                        }
                        else
                        {
                            Console.WriteLine("Reservation not found.");
                        }
                        break;
                    case "6":
                        Console.Write("Enter national ID to pay for: ");
                        string serviceIDPay = Console.ReadLine();

                        Service serviceToPay = HotelSystem.ReturnServiceList().Find(s => s.ID == serviceIDPay);
                        if (serviceToPay != null)
                        {
                            g.PayForService(serviceToPay);
                        }
                        else
                        {
                            Console.WriteLine("Service not found.");
                        }
                        break;
                    case "7":
                        Logout();
                        DisplayLoginScreen();
                        x = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        public void DisplayOptionsMenuForManager()
        {
            bool x = true;
            while (x) { 
            Console.WriteLine("\nManager Options Menu:");
            Console.WriteLine("1. View All Rooms");
            Console.WriteLine("2. View All Reservations");
            Console.WriteLine("3. View All Services");
            Console.WriteLine("4. View All Guests");
            Console.WriteLine("5. View All Payments");
            Console.WriteLine("6. Update a Room");
            Console.WriteLine("7. Generate Profit Report");
            Console.WriteLine("8. Logout");
            Console.Write("Enter your choice: ");
            Manager m = new Manager("m2024", "00");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Showing all rooms...");
                    m.ViewAllRooms(HotelSystem.ReturnRoomList());
                    break;
                case 2:
                    Console.WriteLine("Showing all reservations...");
                    m.ViewAllReservations(HotelSystem.ReturnReservationList());
                    break;
                case 3:
                    Console.WriteLine("Showing all services...");
                    m.ViewAllServices(HotelSystem.ReturnServiceList());
                    break;
                case 4:
                    Console.WriteLine("Showing all guests...");
                    m.ViewAllGuests(HotelSystem.ReturnGuestList());
                    break;
                case 5:
                    Console.WriteLine("Showing all payments...");
                    m.ViewAllPayments(HotelSystem.ReturnPaymentList());
                    break;
                case 6:
                    Console.WriteLine("Updating a room...");
                    string roomNumber = Console.ReadLine();

                    Room roomToUpdate = HotelSystem.ReturnRoomList().Find(r => r.Number == roomNumber);
                    if (roomToUpdate != null)
                    {
                        Console.Write("Enter New Type: ");
                        string newType = Console.ReadLine();
                        Console.Write("Enter New Price: ");
                        double newPrice = double.Parse(Console.ReadLine());
                        Console.Write("Is Room Available? (true/false): ");
                        bool isAvailable = bool.Parse(Console.ReadLine());

                        m.UpdateRoom(roomToUpdate, newType, newPrice, isAvailable);
                    }
                    else
                    {
                        Console.WriteLine("Room not found.");
                    }
                    break;
                case 7:
                    Console.WriteLine("Generating profit report...");
                    m.GenerateProfitReport(HotelSystem.ReturnPaymentList(),HotelSystem.ReturnReservationList(),HotelSystem.ReturnServiceList());
                    break;
                case 8:
                    Logout();
                    User.DisplayLoginScreen();
                    x = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        }
        public override string ToString()
        {
            return "User ID: " + ID;
        }
    }

    // Guest class
    [Serializable]
    class Guest : User
    {
        public string NationalID { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public double BankBalance { get; set; }
        public List<Reservation> Reservations { get; set; }

        public Guest(string nationalID, string name, string password, string phoneNo, double bankBalance)
            : base(nationalID, password)
        {
            NationalID = nationalID;
            Name = name;
            PhoneNo = phoneNo;
            BankBalance = bankBalance;
            Reservations = new List<Reservation>();
        }
        
        public void ReserveRoom(Room room, string checkInDate, string checkOutDate)
        {
            if (room.IsAvailable)
            { 
                Console.Write("Create your reservation ID: ");
                string rID = Console.ReadLine();
                Console.WriteLine("Select meal option:");
                Console.WriteLine("1 - breakfast");
                Console.WriteLine("2 - breakfast and lunch");
                Console.WriteLine("3 - full board");
                Console.Write("Enter your choice: ");
                string mealchoice = Console.ReadLine();
                switch(mealchoice)
                {
                    case "b":
                        mealchoice = "breakfast";
                        break;
                    case "bl":
                        mealchoice = "breakfast and lunch";
                        break;
                    case "fb":
                        mealchoice = "full board";
                        break;
                    default: Console.WriteLine("invalid meal choice");
                        break;
                }
                Reservation r1 = new Reservation(rID, room.Number, NationalID, checkInDate, checkOutDate, "Confirmed", mealchoice,room.BasePrice);
                HotelSystem.StoreReservation(r1);
                Reservations.Add(r1);
                
                room.IsAvailable = false;
                
                List<Room> roomlist = HotelSystem.ReturnRoomList();
                for (int i = 0; i < roomlist.Count; i++)
                {
                    if (roomlist[i].Number == room.Number) 
                    {
                        roomlist[i] = room;
                        break;
                    }
                }
                FileStream fileStream = new FileStream("Rooms.txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (Room updatedRoom in roomlist)
                {
                    formatter.Serialize(fileStream, updatedRoom);
                }
                fileStream.Close();
                Console.WriteLine("Room " + room.Number + " reserved successfully.");
            }
            else
            {
                Console.WriteLine("Room " + room.Number + " is not available.");
            }
        }

        public void RequestService(Service service, Reservation reservation, List<Service> servicesList)
        {
            if (reservation.Status == "Checked-in")
            {
                HotelSystem.StoreService(service);
                servicesList.Add(service);

                FileStream fileStream = new FileStream("Services.txt", FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();

                List<Service> allservices = new List<Service>();
                while (fileStream.Position < fileStream.Length)
                {
                    allservices.Add((Service)formatter.Deserialize(fileStream));
                }
                fileStream.Close();

                allservices.Add(service);

                fileStream = new FileStream("Services.txt", FileMode.Create);
                foreach (Service updatedService in allservices)
                {
                    formatter.Serialize(fileStream, updatedService);
                }
                fileStream.Close();

                Console.WriteLine("Service requested: " + service.Description + " for reservation ID: " + reservation.ID);
            }
            else
            {
                Console.WriteLine("Cannot request a service. Guest must be checked-in.");
            }
        }

        public void CheckIn(Reservation reservation)
        {
            if (reservation.Status == "Confirmed")
            {
                reservation.Status = "Checked-in";

                List<Reservation> rlist = HotelSystem.ReturnReservationList();
                for (int i = 0; i < rlist.Count; i++)
                {
                    if (rlist[i].ID == reservation.ID)
                    {
                        rlist[i] = reservation;
                        break;
                    }
                }

                FileStream fileStream = new FileStream("Reservations.txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (Reservation updatedReservation in rlist)
                {
                    formatter.Serialize(fileStream, updatedReservation);
                }
                fileStream.Close();

                Console.WriteLine("Check-in successful for reservation ID: " + reservation.ID);
            }
            else
            {
                Console.WriteLine("Cannot check in. Reservation is not confirmed.");
            }
        }

        public void CheckOut(Reservation reservation)
        {
            if (reservation.Status == "Checked-in")
            {
                reservation.Status = "Checked-out";

                List<Reservation> rlist = HotelSystem.ReturnReservationList();
                for (int i = 0; i < rlist.Count; i++)
                {
                    if (rlist[i].ID == reservation.ID)
                    {
                        rlist[i] = reservation;
                        break;
                    }
                }

                FileStream fileStream = new FileStream("Reservations.txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (Reservation updatedReservation in rlist)
                {
                    formatter.Serialize(fileStream, updatedReservation);
                }
                fileStream.Close();

                Console.WriteLine("Check-out successful for reservation ID: " + reservation.ID);
            }
            else
            {
                Console.WriteLine("Cannot check out. Guest has not checked in.");
            }
        }
        private void UpdateBankBalanceInFile()
        {
            try
            {
                List<Guest> guestList = HotelSystem.ReturnGuestList();

                for (int i = 0; i < guestList.Count; i++)
                {
                    if (guestList[i].NationalID == this.NationalID)
                    {
                        guestList[i].BankBalance = this.BankBalance;
                        break;
                    }
                }

                FileStream fileStream = new FileStream("Guests.txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (Guest guest in guestList)
                {
                    formatter.Serialize(fileStream, guest);
                }
                fileStream.Close();

                Console.WriteLine("Bank balance updated successfully in the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating bank balance: " + ex.Message);
            }
        }

        public void PayForReservation(Reservation reservation)
        {
            double amount = reservation.TotalAmount;
            if (BankBalance >= amount)
            {
                BankBalance -= amount;
                string status = "Paid";
                UpdateBankBalanceInFile();

                List<Reservation> plist = HotelSystem.ReturnReservationList();
                for (int i = 0; i < plist.Count; i++)
                {
                    if (plist[i].ID == reservation.ID)
                    {
                        plist[i] = reservation;
                        break;
                    }
                }

                FileStream fileStream = new FileStream("Reservations.txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (Reservation updatedReservation in plist)
                {
                    formatter.Serialize(fileStream, updatedReservation);
                }
                fileStream.Close();

                Payment payment = new Payment(Guid.NewGuid().ToString(), NationalID, amount, status);

                try
                {
                    FileStream paymentFileStream = new FileStream("Payments.txt", FileMode.Append);
                    BinaryFormatter paymentFormatter = new BinaryFormatter();
                    paymentFormatter.Serialize(paymentFileStream, payment);
                    paymentFileStream.Close();

                    Console.WriteLine("Payment successful for reservation " + reservation.ID + ". Remaining balance: " + BankBalance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while storing payment: " + ex.Message);
                }

            }
            else
            {
                Console.WriteLine("Insufficient balance for payment.");
            }
        }


        public void PayForService(Service service)
        {
            double amount = service.Cost;
            if (BankBalance >= amount)
            {
                BankBalance -= amount;
                string status = "Paid";
                UpdateBankBalanceInFile();

                List<Service> plist = HotelSystem.ReturnServiceList();
                for (int i = 0; i < plist.Count; i++)
                {
                    if (plist[i].ID == service.ID)
                    {
                        plist[i] = service;
                        break;
                    }
                }

                FileStream fileStream = new FileStream("Services.txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();
                foreach (Service updatedService in plist)
                {
                    formatter.Serialize(fileStream, updatedService);
                }
                fileStream.Close();
                Payment payment = new Payment(Guid.NewGuid().ToString(), NationalID, amount, status);

                try
                {
                    FileStream paymentFileStream = new FileStream("Payments.txt", FileMode.Append);
                    BinaryFormatter paymentFormatter = new BinaryFormatter();
                    paymentFormatter.Serialize(paymentFileStream, payment);
                    paymentFileStream.Close();

                    Console.WriteLine("Payment successful for service . Remaining balance: " + BankBalance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while storing payment: " + ex.Message);
                }

            }
            else
            {
                Console.WriteLine("Insufficient balance for payment.");
            }
        }

        public override string ToString()
        {
            return "[GUEST]\nNational ID: " + NationalID + ", Name: " + Name + ", Phone: " + PhoneNo + ", Bank Balance: " + BankBalance;
        }
    }

    // Manager class
    [Serializable]
    class Manager : User
    {
        public Manager(string id, string password) : base(id, password) { }

        // عرض الكيانات المختلفة من القوائم
        public void ViewAllRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                Console.WriteLine(room.ToString());
            }
        }

        public void ViewAllReservations(List<Reservation> reservations)
        {
            foreach (Reservation reservation in reservations)
            {
                Console.WriteLine(reservation.ToString());
            }
        }

        public void ViewAllServices(List<Service> services)
        {
            foreach (Service service in services)
            {
                Console.WriteLine(service.ToString());
            }
        }

        public void ViewAllGuests(List<Guest> guests)
        {
            foreach (Guest guest in guests)
            {
                Console.WriteLine(guest.ToString());
            }
        }

        public void ViewAllPayments(List<Payment> payments)
        {
            foreach (Payment payment in payments)
            {

                Console.WriteLine(payment.ToString());
            }
        }

        public void UpdateRoom(Room room, string newType, double newPrice, bool newAvailability)
        {
            room.Type = newType;
            room.BasePrice = newPrice;
            room.IsAvailable = newAvailability;

            List<Room> roomList = HotelSystem.ReturnRoomList();
            for (int i = 0; i < roomList.Count; i++)
            {
                if (roomList[i].Number == room.Number)
                {
                    roomList[i] = room;
                    break;
                }
            }

            FileStream fileStream = new FileStream("Rooms.txt", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            foreach (Room updatedRoom in roomList)
            {
                formatter.Serialize(fileStream, updatedRoom);
            }
            fileStream.Close();

            Console.WriteLine("Room " + room.Number + " has been updated and saved to file.");
        }

        public void GenerateProfitReport(List<Payment> payments, List<Reservation> reservations, List<Service> services)
        {
            double totalRoomIncome = 0;
            double totalCarRentalIncome = 0;
            double totalKidsZoneIncome = 0;
            double totalIncome = 0;

            // التحقق من وجود مدفوعات
            if (payments == null || payments.Count == 0)
            {
                Console.WriteLine("No payments found to generate the profit report.");
                return;
            }

            // معالجة المدفوعات
            foreach (Payment payment in payments)
            {
                if (payment.Status == "Paid")
                {
                    // البحث عن الفئة بناءً على RelatedID
                    if (reservations.Exists(r => r.ID == payment.RelatedID))
                    {
                        // مرتبط بحجز غرفة
                        totalRoomIncome += payment.Amount;
                    }
                    else if (services.Exists(s => s.ID == payment.RelatedID && s.Description.Contains("Car Rental")))
                    {
                        // مرتبط بتأجير سيارة
                        totalCarRentalIncome += payment.Amount;
                    }
                    else if (services.Exists(s => s.ID == payment.RelatedID && s.Description.Contains("Kids Z one")))
                    {
                        // مرتبط بخدمات الأطفال
                        totalKidsZoneIncome += payment.Amount;
                    }
                }
            }

            // حساب الدخل الإجمالي
            totalIncome = totalRoomIncome + totalCarRentalIncome + totalKidsZoneIncome;

            // عرض التقرير
            Console.WriteLine("===== Profit Report =====");
            Console.WriteLine("- Room Reservations: " + totalRoomIncome);
            Console.WriteLine("- Car Rental: " + totalCarRentalIncome);
            Console.WriteLine("- Kids Zone: " + totalKidsZoneIncome);
            Console.WriteLine("- Total Income: " + totalIncome);
            Console.WriteLine("=========================");

            // حفظ التقرير باستخدام BinaryFormatter
            try
            {
                FileStream fileStream = new FileStream("profit_report.txt", FileMode.Create);
                BinaryFormatter formatter = new BinaryFormatter();

                // تسلسل البيانات وحفظها
                formatter.Serialize(fileStream, totalRoomIncome);
                formatter.Serialize(fileStream, totalCarRentalIncome);
                formatter.Serialize(fileStream, totalKidsZoneIncome);
                formatter.Serialize(fileStream, totalIncome);

                fileStream.Close();

                Console.WriteLine("Profit report has been generated and saved to 'profit_report.txt'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating profit report: " + ex.Message);
            }
        }





        public override string ToString()
        {
            return "Manager ID: " + ID;
        }
    }

    // Room class
    [Serializable]
    class Room
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public double BasePrice { get; set; }
        public bool IsAvailable { get; set; }

        public Room(string number, string type, double basePrice, bool isAvailable)
        {
            Number = number;
            Type = type;
            BasePrice = basePrice;
            IsAvailable = isAvailable;
        }

        public void UpdateInformation(string type, double price, bool availability)
        {
            Type = type;
            BasePrice = price;
            IsAvailable = availability;
        }
        public override string ToString()
        {
            return "[ROOM]\nNumber: " + Number + ", Type: " + Type + ", Price: " + BasePrice +", Available: " + (IsAvailable ? "Yes" : "No");
        }
    }

    // Reservation class
    [Serializable]
    class Reservation
    {
        private string id;
        private string status;
        private string meals;

        public string ID
        {
            get { return id; }
            set
            {
                if (value.Length == 4 && IsNumeric(value))
                {
                    id = value;
                }
                else
                {
                    Console.WriteLine("Reservation ID must be a 4-digit number.");
                }
            }
        }

        public string RoomNumber { get; set; }
        public string GuestNationalID { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string BillNumber { get; set; }

        public string Status
        {
            get { return status; }
            set
            {
                if (value == "Confirmed" || value == "Checked-in" || value == "Checked-out")
                {
                    status = value;
                }
                else
                {
                    Console.WriteLine("Invalid status value. Must be 'Confirmed', 'Checked-in', or 'Checked-out'.");
                }
            }
        }

        public string Meals
        {
            get { return meals; }
            set
            {
                if (value == "breakfast" || value == "breakfast and lunch" || value == "full board")
                {
                    meals = value;
                }
                else
                {
                    Console.WriteLine("Invalid meal option. Must be 'breakfast', 'breakfast and lunch', or 'full board'.");
                }
            }
        }

        public double Discount { get; private set; }
        public double TotalAmount { get; private set; }

        public Reservation(string id, string roomNumber, string guestNationalID, string checkInDate, string checkOutDate, string status, string meals, double roomPrice)
        {
            ID = id;
            RoomNumber = roomNumber;
            GuestNationalID = guestNationalID;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = status;
            Meals = meals;
            CalculateTotalAmount(roomPrice);
        }

        private void CalculateTotalAmount(double roomPrice)
        {
            // حساب عدد أيام الإقامة
            DateTime checkIn = DateTime.Parse(CheckInDate);
            DateTime checkOut = DateTime.Parse(CheckOutDate);
            int days = (checkOut - checkIn).Days;

            // حساب التكلفة بناءً على نوع الوجبات
            if (Meals == "breakfast")
            {
                TotalAmount = days * roomPrice;
            }
            else if (Meals == "breakfast and lunch")
            {
                TotalAmount = 1.2 * (days * roomPrice);
            }
            else if (Meals == "full board")
            {
                TotalAmount = 1.4 * (days * roomPrice);
            }

            // تطبيق الخصم إذا كان تاريخ الدخول ضمن التواريخ المحددة
            if (CheckDiscountDate(checkIn))
            {
                Discount = 0.6 * TotalAmount;
                TotalAmount -= Discount;
            }
            else
            {
                Discount = 0;
            }
        }

        private bool CheckDiscountDate(DateTime date)
        {
            return date == new DateTime(2025, 2, 1) ||
                   date == new DateTime(2025, 4, 22) ||
                   date == new DateTime(2025, 10, 10);
        }

        private bool IsNumeric(string value)
        {
            foreach (char c in value)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return "[Reservation]\nID: " + ID + ", Room: " + RoomNumber + ", Guest: " + GuestNationalID +
                   ", Check-in: " + CheckInDate + ", Check-out: " + CheckOutDate +
                   ", Status: " + Status + ", Meals: " + Meals + ", Total Amount: " + TotalAmount +
                   ", Discount: " + Discount;
        }
    }

    // Service class
    [Serializable]
    class Service
    {
        public string ID { get; set; }
        public string ReservationID { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string Notes { get; set; }

        public Service(string id, string reservationID, string description, int count)
        {
            ID = id;
            ReservationID = reservationID;
            Description = description;

            if (description.ToLower() == "car rental")
            {
                Cost = count * 10; 
                Notes = "number of rental days = " + count;
            }
            else if (description.ToLower() == "kids zone")
            {
                Cost = count * 5; 
                Notes = "number of children = " + count;
            }
            else
            {
                Cost = 0;
                Notes = "Invalid service type";
            }
        }

        public override string ToString()
        {
            return "[Service]\nID: " + ID + ", Reservation ID: " + ReservationID + ", Description: " + Description + ", Cost: " + Cost + ", Notes: " + Notes;
        }
    }

    // Payment class
    [Serializable]
    class Payment
    {
        public string BillNumber { get; set; } 
        public string RelatedID { get; set; } 
        public double Amount { get; set; } 
        public string Status { get; set; } 

        public Payment(string billNumber, string relatedID, double amount,string status)
        {
            BillNumber = billNumber;
            RelatedID = relatedID;
            Amount = amount;
            Status = status;
        }

        public void MakePayment(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Payment amount must be greater than 0.");
                return;
            }

            if (Status == "paid")
            {
                Console.WriteLine("This bill is already fully paid.");
                return;
            }

            Amount -= amount;

            if (Amount <= 0)
            {
                Amount = 0;
                Status = "paid"; // Change status to "paid"
                Console.WriteLine("Payment completed successfully.");
            }
            else
            {
                Console.WriteLine("Partial payment made. Remaining amount: " + Amount);
            }
        }

        public override string ToString()
        {
            return "[Payment]\nBill Number: " + BillNumber + ", Related ID: " + RelatedID + ", Amount: " + Amount + ", Status: " + Status;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            HotelSystem hotelSystem = new HotelSystem();

            //Guest guest1 = new Guest("12345", "Omar", "11", "05215", 450);
            //HotelSystem.StoreGuest(guest1);
            //Guest guest2 = new Guest("12546", "Khaled", "22", "01459", 550);
            //HotelSystem.StoreGuest(guest2);
            //Guest guest3 = new Guest("16556", "Salma", "33", "04122", 660);
            //HotelSystem.StoreGuest(guest3);
            //Guest guest4 = new Guest("18730", "Ahmad", "44", "02250", 720);
            //HotelSystem.StoreGuest(guest4);



            //Room room1 = new Room("442", "single", 25, true);
            //HotelSystem.StoreRoom(room1);
            //Room room2 = new Room("102", "double", 30, true);
            //HotelSystem.StoreRoom(room2);
            //Room room3 = new Room("506", "double", 32, false);
            //HotelSystem.StoreRoom(room3);
            //Room room4 = new Room("702", "suite", 40, true);
            //HotelSystem.StoreRoom(room4);
            //Room room5 = new Room("333", "double", 34, true);
            //HotelSystem.StoreRoom(room5);


            User.DisplayLoginScreen();
        }
    }
}
