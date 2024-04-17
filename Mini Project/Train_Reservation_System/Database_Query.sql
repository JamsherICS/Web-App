create database Train_Reservation_DB

--table for storing train details
create table train_details (
    trainNo numeric(5) primary key,
    trainName varchar(100) not null,
    "From" varchar(100) not null,
    "To" varchar(100) not null,
    "Status" varchar(20) default 'Active'
)

--ALTER TABLE train_details ADD isDeleted BIT NOT NULL DEFAULT 0;
--ALTER TABLE train_details ADD FromTiming VARCHAR(8), ToTiming VARCHAR(8);


select *from train_details

--table for storing train classes
create table train_classes (
    serialNo int identity primary key,
    trainNo numeric(5) foreign key references train_details(trainNo) not null,
    "1AC" float not null,
    "2AC" float not null,
    "3AC" float not null,
    SL float not null
)


--store sear availability
create table seat_availability (
    serialNo int identity primary key,
    trainNo numeric(5) foreign key references train_details(trainNo),
    "1AC" int not null,
    "2AC" int not null,
    "3AC" int not null,
    SL int not null
)

--drop table seat_availability


--table for storing user details
create table user_details (
    userId int primary key,
    userName varchar(20) not null,
    age int,
    passcode varchar(20) not null
)

--ALTER TABLE user_details ADD mobile_number VARCHAR(20);


--admin details table to store admin data
create table admin_details (
    adminId int primary key,
    adminName varchar(20) not null,
    passcode varchar(20) not null
)

-- Inserting values into train_details table for testing
insert into train_details values 
(12107, 'SP Express', 'Delhi', 'Gorakhpur', 'Active'),
(12167, 'Barauni Express', 'Gorakhpur', 'Bihar', 'Active')

-- Inserting values into train_classes table for testing
insert into train_classes values 
(12107, 4000, 2500, 1500, 450),
(12167, 5000, 3000, 2000, 500)

-- Inserting values into seat_availability table for testing
insert into seat_availability values 
(12107, 50, 50, 150, 250),
(12167, 60, 40, 120, 400);

-- Inserting values into user_details table for testing
insert into user_details values 
(111, 'Srk', 50, 'srk123'),
(222, 'Salman', 51, 'salman123');

-- Inserting values into admin_details table for testing
insert into admin_details values 
(101, 'jamsher', '8922'),
(102, 'admin', 'admin');


--table for store booked tickets
create table booked_ticket (
    PNR int primary key,
    userId int foreign key references user_details(userId),
    trainNo numeric(5) foreign key references train_details(trainNo),
    passengerName varchar(100) not null,
    passengerAge int not null,
    ticketClass varchar(20) not null,
    totalFare float not null,
    bookingDateTime datetime not null
)


--table for store canceled tickets
create table canceled_ticket (
    canceledId int primary key,
    PNR int foreign key references booked_ticket(PNR),
    userId int foreign key references user_details(userId),
    trainNo numeric(5) foreign key references train_details(trainNo),
    cancellationDateTime datetime not null,
    refundAmount float not null
)



--this query for checking
select * from train_details
select * from train_classes
select * from seat_availability
select * from user_details
select * from admin_details
select * from booked_ticket
select * from canceled_ticket
