DROP SCHEMA IF EXISTS `bakingbunny` ;
-- -----------------------------------------------------
-- Schema hha
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `bakingbunny` DEFAULT CHARACTER SET latin1 ;

use bakingbunny; -- Change database name as required.

-- Category
CREATE TABLE Category
(
    Id int NOT NULL AUTO_INCREMENT,
    Name varchar(50) NOT NULL,
    PRIMARY KEY (Id)
);

-- Size
CREATE TABLE Size
(
    Id int NOT NULL AUTO_INCREMENT,
    SizeName varchar(50) NOT NULL,
    PRIMARY KEY (Id)
);

-- Fruit
CREATE TABLE Taste
(
    Id int NOT NULL AUTO_INCREMENT,
    TasteName varchar(255) NOT NULL,
    PRIMARY KEY (Id)
) CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci; -- utf8 and utf8_unicode_ci are deprecated

-- CakeType
CREATE TABLE CakeType
(
    Id int NOT NULL AUTO_INCREMENT,
    Type varchar(50) NOT NULL,
    PRIMARY KEY (Id)
);


-- User
CREATE TABLE User
(
    Id int NOT NULL AUTO_INCREMENT,
    Firstname varchar(50) NOT NULL,
    Lastname varchar(50) NOT NULL,
    Email varchar(200) NOT NULL,
    Address varchar(200) NOT NULL,
    PostalCode varchar(10) NOT NULL,
    Phone varchar(15) NOT NULL,
    City varchar(10) NOT NULL,
    PRIMARY KEY (Id)
);

-- CustomOrder
CREATE TABLE CustomOrder
(
    Id int NOT NULL AUTO_INCREMENT,
    ExampleImage varchar(200) NULL,
    Message varchar(200) NULL,
    UserId int NOT NULL,
    SizeId int NOT NULL,
    TasteId int NOT NULL,
    CakeTypeId int NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_customOrder_user FOREIGN KEY (UserId) REFERENCES User (Id),
    CONSTRAINT fk_customOrder_size FOREIGN KEY (SizeId) REFERENCES Size (Id),
    CONSTRAINT fk_customOrder_taste FOREIGN KEY (TasteId) REFERENCES Taste (Id),
    CONSTRAINT fk_customOrder_cakeType FOREIGN KEY (CakeTypeId) REFERENCES CakeType (Id)
);

-- Product
CREATE TABLE Product
(
    Id int NOT NULL AUTO_INCREMENT,
    Name varchar(50) NOT NULL,
    Price float NOT NULL,
    Description varchar (255) NULL,
    ProductImage varchar(200) NULL,
    Comment varchar(255) NULL,
    Active bit NOT NULL,
    CategoryId int NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_product_category FOREIGN KEY (CategoryId) REFERENCES Category (Id)
) CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci; -- utf8 and utf8_unicode_ci are deprecated

-- OrderList
CREATE TABLE OrderList
(
    Id int NOT NULL AUTO_INCREMENT,
    Subtotal float NOT NULL,
    DeliveryFee float NOT NULL DEFAULT 0,
    Total float NOT NULL,
    Delivery bit NOT NULL,
    OrderDate datetime NOT NULL,
    PickupDeliveryDate datetime NOT NULL,
    Allergy varchar(255) NULL,
    Comment varchar(255) NULL,
    UserId int NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_orderList_user FOREIGN KEY (UserId) REFERENCES User (Id)
);

-- SaleItem
CREATE TABLE SaleItem
(
    Id int NOT NULL AUTO_INCREMENT,
    Quantity int NOT NULL,
    Discount float DEFAULT 0,
    ProductId int NOT NULL,
    SizeId int NOT NULL,
    TasteId int NOT NULL,
    OrderListId int NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_saleItem_product FOREIGN KEY (ProductId) REFERENCES Product (Id),
    CONSTRAINT fk_saleItem_size FOREIGN KEY (SizeId) REFERENCES Size (Id),
    CONSTRAINT fk_saleItem_taste FOREIGN KEY (TasteId) REFERENCES Taste (Id),
    CONSTRAINT fk_saleItem_orderList FOREIGN KEY (OrderListId) REFERENCES OrderList (Id)
);


CREATE TABLE ErrorLog
(
    id int not null AUTO_INCREMENT,
    errorDate datetime not null,
    message text not null,
    level varchar(50) not null,
    additionalInfo varchar(255) null,
    PRIMARY KEY (id)
);


-------------------- INSERT ---------------------------

-- Default data for Fruit
INSERT INTO Taste VALUES (NULL, 'Strawberry');
INSERT INTO Taste VALUES (NULL, 'Mango');
INSERT INTO Taste VALUES (NULL, 'None (Fresh Milk)');
INSERT INTO Taste VALUES (NULL, 'Cafe Latte 카페라떼');
INSERT INTO Taste VALUES (NULL, 'Choco Nutella 초코누텔라');
INSERT INTO Taste VALUES (NULL, 'Black-Sesame 흑임자');
INSERT INTO Taste VALUES (NULL, 'Lotus 로투스');
INSERT INTO Taste VALUES (NULL, 'Cheese 뽀또');
INSERT INTO Taste VALUES (NULL, 'Ang Butter 앙버터');

-- Default data for Size
INSERT INTO Size VALUES (NULL, '6"');
INSERT INTO Size VALUES (NULL, '8"');

-- Default data for CakeType
INSERT INTO CakeType VALUES (NULL, 'Butter');
INSERT INTO CakeType VALUES (NULL, 'Wheap Cream');

-- Default data for Category
INSERT INTO Category VALUES (NULL, 'Cake');
INSERT INTO Category VALUES (NULL, 'Dacquoise');

-- Default data for Product
-- Cake
INSERT INTO Product VALUES (NULL, 'Black Sesame 흑임자', 45, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/cake_black.jpg', null, 1, 1);
INSERT INTO Product VALUES (NULL, 'Fresh Whiped Cream 생크림', 41, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/cake_fresh.jpg', null, 1, 1);
INSERT INTO Product VALUES (NULL, 'Injeolmi 인절미', 45, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/cake_injeol.jpg', null, 1, 1);
INSERT INTO Product VALUES (NULL, 'Mugwort 쑥절미', 45, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/cake_ssook.jpg', null, 1, 1);
INSERT INTO Product VALUES (NULL, 'Kabocha Cheese 단호박 치즈', 45, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/kambocha.jpg', null, 1, 1);
INSERT INTO Product VALUES (NULL, 'Injeolmi 인절미', 4.1, 'Description here', 'TBD', null, 0, 2);

-- Dacquoise
INSERT INTO Product VALUES (NULL, 'Custom', 0, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/custom.jpg', null, 1, 1);
INSERT INTO Product VALUES (NULL, 'Blueberry 블루베리', 3.9, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Early Grey 얼그레이', 4.2, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/dacq_earl.jpg', null, 1, 2);
INSERT INTO Product VALUES (NULL, 'Cherry 체리', 3.9, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Black Sesame 흑임자', 4.1, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Maltesers 초코몰티져스', 3.7, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Lotus 로투스', 4.1, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/dacq_lotus.jpg', null, 1, 2);
INSERT INTO Product VALUES (NULL, 'Matcha Kitkat 말차킷캣', 3.8, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Oreo 오레오', 4.1, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/dacq_oreo.jpg', null, 1, 2);
INSERT INTO Product VALUES (NULL, 'Mugwort 쑥', 4, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/dacq_ssook.jpg', null, 1, 2);
INSERT INTO Product VALUES (NULL, 'Sweet Potato 찐고구마', 4.1, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Cafe Latte 카페라떼', 4, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/dacq_cafe.jpg', null, 1, 2);
INSERT INTO Product VALUES (NULL, 'Salted Caremel 솔티카라멜', 3.7, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Tiramisu 티라미수', 4.1, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Bacon 베이컨', 3.7, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Tok-tok Corn 톡톡옥수수', 4, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Ang Butter 앙버터', 4, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Cheese 뽀또', 4, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Chai Latte 차이라테', 3.9, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Matcha Nutella 녹차누텔라', 4, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Citron 유자', 3.9, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Yogurt 요거트', 3.9, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Choco Nutella 초코누텔라', 3.9, 'Description here', 'TBD', null, 0, 2);
INSERT INTO Product VALUES (NULL, 'Dacquoise combo 다쿠아즈콤보', 20, 'Description here', 'https://bbproject20210623.s3.us-west-2.amazonaws.com/dac_set.jpg', null, 1, 2);