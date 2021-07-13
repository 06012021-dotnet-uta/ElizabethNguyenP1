CREATE DATABASE P0_Database;

CREATE TABLE Customers
(
	CustomerID int NOT NULL IDENTITY(1, 1),
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	UserName varchar(50) NOT NULL UNIQUE,
	UserPassword varchar(50) NOT NULL,
	UserEmail varchar(150) NOT NULL,
	Age int NOT NULL,
	SecretUser BIT,
	DefaultStore int DEFAULT 1,

	CONSTRAINT PK_CustomersID PRIMARY KEY (CustomerID ASC),
	CONSTRAINT FK_LocationID FOREIGN KEY (DefaultStore) REFERENCES Locations(LocationID)

);
GO

CREATE TABLE Locations
(
	LocationID int NOT NULL IDENTITY(1, 1),
	LocationAddress varchar(150) NOT NULL,
	LocationName varchar(50) NOT NULL,

	CONSTRAINT PK_LocationID PRIMARY KEY (LocationID ASC)
);
GO

CREATE TABLE Orders
(
	OrderID int NOT NULL IDENTITY(1, 1),
	LocationID int NOT NULL,
	CustomerID int NOT NULL,
	OrderDate DATETIME NOT NULL,

	CONSTRAINT PK_OrderID PRIMARY KEY (OrderID DESC),
	CONSTRAINT FK_OrderLocationID FOREIGN KEY (LocationID) REFERENCES Locations(LocationID),
	CONSTRAINT FK_OrderCustomerID FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);
GO

CREATE TABLE Products
(
	ProductID int NOT NULL IDENTITY(1, 1),
	ProductName varchar(50) NOT NULL,
	ProductDescription varchar(250) NOT NULL,
	Price float NOT NULL,
	ImgPath varchar(200) NOT NULL,

	CONSTRAINT PK_ProductID PRIMARY KEY (ProductID ASC)
);
GO

CREATE TABLE Inventory
(
	LocationID int NOT NULL,
	ProductID int NOT NULL,
	Amount int NOT NULL,

	CONSTRAINT PK_Inventory PRIMARY KEY (LocationId, ProductID),
	CONSTRAINT FK_InventoryLocationID FOREIGN KEY (LocationID) REFERENCES Locations(LocationID),
	CONSTRAINT FK_InventoryProductID FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
GO

CREATE TABLE OrderedProducts
(
	OrderID int NOT NULL,
	ProductID int NOT NULL,
	Amount int NOT NULL

	CONSTRAINT PK_OrderedProducts PRIMARY KEY (OrderID, ProductID),
	CONSTRAINT FK_OrderID FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
	CONSTRAINT FK_ProductID FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
GO

INSERT INTO Orders(LocationID, CustomerID, OrderDate)
VALUES ('3','3', GETDATE());

SELECT * FROM Orders;

CREATE TABLE Tags
(
	TagID int NOT NULL IDENTITY(1, 1),
	ShortName varchar(15) NOT NULL,
	LongName varchar(50) NOT NULL,

	CONSTRAINT PK_TagID PRIMARY KEY (TagID ASC)
);
GO

CREATE TABLE ProductTags
(
	ProductID int NOT NULL,
	TagID int NOT NULL,

	CONSTRAINT PK_ProductTags PRIMARY KEY (ProductID, TagID),
	CONSTRAINT FK_ProductIDTag FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
	CONSTRAINT FK_TagID FOREIGN KEY (TagID) REFERENCES Tags(TagID)
);
GO

/*
CREATE TABLE Coupons
(
	CouponID int NOT NULL IDENTITY(1, 1),
	FlatAmount int NOT NULL,
	PercentageAmount double(3) NOT NULL,
	ItemUpperLimit int NOT NULL,
	ItemLowerLimit int NOT NULL,

	CONSTRAINT PK_CouponID PRIMARY KEY (CouponID ASC)
);
GO
*/

SELECT * FROM Customers;
SELECT * FROM Locations;
SELECT * FROM Products;
SELECT * FROM Inventory;
SELECT * FROM OrderedProducts;
SELECT * FROM Orders;
SELECT * FROM Tags;

INSERT INTO Customers (FirstName, LastName, UserName, UserPassword, UserEmail, Age, SecretUser, DefaultStore)
VALUES
	('Guest', 'User', 'VOID', 'VOID', 'VOID@VOID.com', '1', 0,1)

INSERT INTO Customers (FirstName, LastName, UserName, UserPassword, UserEmail, Age, SecretUser)
VALUES
	('Elizabeth', 'Nguyen', 'liznguyen12', 'asd', 'liznguyen12@gmail.com', '25', 1),
	('Potato', 'Dandy', 'potatoDandy', 'awd', 'potato@gmail.com', 30, 0),
	('Daisy', 'LoveJoy', 'LovelyDaisy845', 'dwa', 'lovejoy@gmail.com', 35, 0),
	('Elizabeth', 'Lucian', 'leophinity', 'asd', 'elizabethnguyen816@gmail.com', 17, 1),
	('Tsina', 'Amamiyan', 'Ralrak', 'dsa', 'ralrakuan@yahoo.com', 19, 0),
	('Paige', 'Campbell', 'PaigeCampbell', 'sef', 'waige_camp@gmail.com', 31, 0),
	('Jonathan', 'Gilley', 'MoonMoon', 'fes', 'moon_joon@gmail.com', 22, 0),
	('Matthew', 'Chew', 'SetsunaGN', 'sdf', 'chew_matt@gmail.com', 23, 0),
	('Love', 'Galore', '4DaTeaClear', 'fds', 'love_galore@gmail.com', 28, 0),
	('Julie', 'Chen', 'MintxLeafy', 'zsc', 'mintxleafy@yahoo.com', 23, 0),
	('Yitz', 'Deng', 'zintegy', 'csz', 'zindeng@a.com', 23, 0),
	('Chris', 'Huang', 'Huang', 'zxc', 'cheffy@food.co', 6, 0),
	('a', 'a', 'a', 'a', 'a@a.com', 1, 0),
	('Elizabeth', 'Nguyen', 'beth_guy', 'vbn', 'e@e.e', 14, 1);

INSERT INTO Tags (ShortName, LongName)
VALUES
	('Burger', 'Signature Umai Burgers'),
	('Drink', 'Signature Umai Drink'),
	('Hotdog', 'Signature Umai Hotdog'),
	('Appetizer', 'Umai House Specials'),
	('Dessert','In-House Desserts');

INSERT INTO Locations (LocationAddress, LocationName)
VALUES 
	('1158 N Capitol Ave, San Jose, CA 95132', 'San Jose'),
	('6600 Topanga Canyon Blvd #1098A, Canoga Park, CA 91303', 'Canoga Park'),
	('3000 Benton St, Santa Clara, CA 95051', 'Santa Clara'),
	('3250 Monroe St, Santa Clara, CA 95053', 'Wilcox'),
	('23622 Rockfield Blvd, Suite 703, Lake Forest CA 92630', 'Lake Forest'),
	('8443 Haven Ave., Rancho Cucamonga, CA 91730', 'Rancho Cucamonga');

INSERT INTO Products (ProductName, Price, ProductDescription, ImgPath)
VALUES 
	('Tokyo Signature Hotdog', 8.99, 'All Beef Hot Dog, Caramelized Onions, Roasted Seaweed, Teri Mayo, Umai Teriyaki Sauce, White Sesame', '~/Images/TokyoSignature.png'),
	('Shinjuku Shadow Hotdog', 9.99, 'Bratwurst, Diced Daikon, Spicy Cheddar Sauce, Umai Teriyaki Sauce, Bonito Flakes, Green Onion, Furikake', '~/Images/ShinjukuShadow.png'),
	('California Bae Hotdog', 10.57, 'Chicken Hot Dog, Avocado, Sliced Cucumber, Feta Cheese, Creamy Sauce, Umai Teriyaki, White Sesame', '~/Images/CaliforniaBae-2.png'),
	('The Merican Hanbagas', 8.99, '1/4 lb Angus Beef Pattie, Umami Glaze, American Cheese, Diced Onions, Roma Tomatoes, Lettuce, Pickles, House Burger Sauce, Brioche Bun', '~/Images/merican.png'),
	('Fried Banana Dog', 7.50, 'Toasted Brioche bun with Umai sweet butter spread, banana wrapped in lumpia roll, deep fried, topped with whip cream, caramel and chocolate drizzle', '~/Images/BananaDog.png'),
	('Busan Boom', 8.95, '1/4 lb Angus Beef Pattie, Umami Glaze, Caramelized Onions, Cheddar Cheese, Umai Teriyaki, Kimchi Relish, String Frites, Brioche Bun', '~/Images/busan-boom.png'),
	('Inu Rangoons', 6.34, 'Wonton wrapped  with cream cheese, variety of hot dogs and cheeses with your choice of dips. Choose between our Yokohama, Tokyo, Sapporo, or Tijuana', '~/Images/InuRangoons.png'),
	('BIG Mozzarella', 8.99,'Korean style BIG Mozzarella Cheese Sticks are guaranteed to leave you smiling. Get them with mustard and ketchup or our marinara dipping sauce','~/Images/Korean-Cheese.png'),
	('Kyoto Baga',9.99,'1/4 lb Angus Beef Pattie, Umami Glaze, American Cheese, Batter Fried Onions, Creamy Habanero Sauce, Jalapenos, Brioche Bun','~/Images/kyoto-baga.png'),
	('Ramune Cream Soda', 6.99,'Choose between a vanilla or butterscotch base and customize your own flavors.', '~/Images/UmaiRamuneCreamSodas.png'),
	('Iced Tea', 6.99, 'Anti-oxidant, fat-lowering, freshly brewed iced teas. Sweetened or unsweetened in your choice of green or black tea with nine specialty flavors to choose from.', '~/Images/UmaiIcedTeas.png');

INSERT INTO Inventory (LocationID, ProductID, Amount)
VALUES
	(1, 1, 30),
	(1, 3, 30),
	(1, 4, 30),
	(1, 5, 30),
	(1, 6, 30),
	(1, 7, 30),
	(1, 8, 30),
	(1, 9, 30),
	(1, 10, 30),
	(1, 11, 30),
	(2, 1, 50),
	(2, 2, 50),
	(2, 4, 50),
	(2, 5, 50),
	(2, 6, 50),
	(2, 7, 50),
	(2, 8, 50),
	(2, 9, 50),
	(2, 10, 50),
	(2, 11, 50),
	(3, 1, 60),
	(3, 2, 60),
	(3, 3, 60),
	(3, 5, 60),
	(3, 6, 60),
	(3, 7, 60),
	(3, 8, 60),
	(3, 9, 60),
	(3, 10, 60),
	(3, 11, 60),
	(4, 1, 60),
	(4, 4, 60),
	(4, 5, 60),
	(4, 6, 60),
	(4, 7, 60),
	(4, 8, 60),
	(4, 9, 60),
	(4, 10, 60),
	(4, 11, 60),
	(5, 2, 60),
	(5, 3, 60),
	(5, 4, 60),
	(5, 5, 60),
	(5, 6, 60),
	(5, 7, 60),
	(5, 8, 60),
	(5, 9, 60),
	(5, 10, 60),
	(5, 11, 60),
	(6, 1, 60),
	(6, 2, 60),
	(6, 3, 60),
	(6, 4, 60);

INSERT INTO Orders (LocationID, CustomerID, OrderDate)
VALUES ('1', '20', GETDATE()),
('2', '20', GETDATE()),
('3', '20', GETDATE()),
('4', '20', GETDATE()),
('5', '20', GETDATE()),
('6', '20', GETDATE()),
('1', '20', GETDATE()),
('2', '20', GETDATE()),
('3', '20', GETDATE()),
('4', '20', GETDATE()),
('5', '20', GETDATE()),
('6', '20', GETDATE());

DROP TABLE ProductTags;
DROP TABLE Tags;
DROP TABLE Orders;
DROP TABLE OrderedProducts;
DROP TABLE Inventory;
DROP TABLE Locations;
DROP TABLE Products;
DROP TABLE Customers;

