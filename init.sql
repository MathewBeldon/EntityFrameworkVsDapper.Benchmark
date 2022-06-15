DROP DATABASE IF exists ef_dapper_benchmark;
CREATE DATABASE ef_dapper_benchmark;

USE ef_dapper_benchmark;

CREATE TABLE Materials (
    Id INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(255),
    Description VARCHAR(255),
    Cost INT,
    CreatedDateUtc DATETIME,
    ModifiedDateUtc DATETIME,
    PRIMARY KEY (Id)
); 

CREATE INDEX IX_MATERIALS_NAME
ON Materials (Name);

drop procedure if exists seedMaterials;
DELIMITER //  
CREATE PROCEDURE seedMaterials()
BEGIN
    DECLARE i int DEFAULT 0;
    WHILE i < 10000 DO
        INSERT INTO Materials
        (
            Name,
            Description,
            Cost,
            CreatedDateUtc
        ) 
        VALUES 
        (
            (SELECT concat("Material ", i)),
            (SELECT concat("Material description but with extra letters ", i)), 
            (SELECT FLOOR(RAND()*(10-5+1)+5)),
            UTC_TIMESTAMP()
        );
        SET i = i + 1;
    END WHILE;
END;
//

CALL seedMaterials();


CREATE TABLE Brands (
    Id INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(255),
    Description VARCHAR(255),
    CreatedDateUtc DATETIME,
    ModifiedDateUtc DATETIME,
    PRIMARY KEY (Id)
); 

CREATE INDEX IX_BRAND_NAME
ON Brands (Name);

drop procedure if exists seedBrands;
DELIMITER //  
CREATE PROCEDURE seedBrands()
BEGIN
    DECLARE i int DEFAULT 0;
    WHILE i < 10000 DO
        INSERT INTO Brands
        (
            Name,
            Description,
            CreatedDateUtc
        ) 
        VALUES
        (
            (SELECT concat("Brand ", i)),
            (SELECT concat("Brand description but with extra letters ", i)),
            UTC_TIMESTAMP()
        );
        SET i = i + 1;
    END WHILE;
END;
//

CALL seedBrands();


CREATE TABLE Styles (
    Id INT NOT NULL AUTO_INCREMENT,
    BrandId INT NOT NULL,
    Name VARCHAR(255),
    Description VARCHAR(255),
    CreatedDateUtc DATETIME,
    ModifiedDateUtc DATETIME,
    PRIMARY KEY (Id),
    FOREIGN KEY (BrandId) REFERENCES Brands(Id)
); 

CREATE INDEX IX_STYLES_NAME
ON Styles (Name);

drop procedure if exists seedStyles;
DELIMITER //   
CREATE PROCEDURE seedStyles()
BEGIN
    DECLARE i int DEFAULT 0;
    WHILE i < 10000 DO
        INSERT INTO Styles
        (
            BrandId,
            Name,
            Description,
            CreatedDateUtc
        )
        VALUES
        (
            (SELECT FLOOR(RAND()*(10000-1+1)+1)),
            (SELECT concat("Style ", i)),
            (SELECT concat("Style description but with extra letters ", i)),
            UTC_TIMESTAMP()
        );
        SET i = i + 1;
    END WHILE;
END;
//

CALL seedStyles();


CREATE TABLE Benches (
    Id INT NOT NULL AUTO_INCREMENT,
    MaterialId INT NOT NULL,
    StyleId INT NOT NULL,
    Name VARCHAR(255),
    Description VARCHAR(255),
    Cost INT,
    Height INT,
    Width INT,
    Depth INT,
    CreatedDateUtc DATETIME,
    ModifiedDateUtc DATETIME,
    PRIMARY KEY (Id),
    FOREIGN KEY (MaterialId) REFERENCES Materials(Id),
    FOREIGN KEY (StyleId) REFERENCES Styles(Id)
); 

CREATE INDEX IX_BENCHES_NAME
ON Benches (Name);

drop procedure if exists seedBenches;
DELIMITER //  
CREATE PROCEDURE seedBenches()
BEGIN
    DECLARE i int DEFAULT 0;
    WHILE i < 10000 DO
        INSERT INTO Benches
        (
            MaterialId,
            StyleId,
            Name,
            Description,
            Cost,
            Height,
            Width,
            Depth,
            CreatedDateUtc
        ) 
        VALUES 
        (
            (SELECT FLOOR(RAND()*(10000-1+1)+1)),
            (SELECT FLOOR(RAND()*(10000-1+1)+1)),
            (SELECT concat("Bench ", i)),
            (SELECT concat("Bench description but with extra letters ", i)), 
            (SELECT FLOOR(RAND()*(1000-50+1)+50)),
            (SELECT FLOOR(RAND()*(10-5+1)+5)),
            (SELECT FLOOR(RAND()*(10-5+1)+5)),
            (SELECT FLOOR(RAND()*(10-5+1)+5)),
            UTC_TIMESTAMP()
        );
        SET i = i + 1;
    END WHILE;
END;
//
CALL seedBenches();