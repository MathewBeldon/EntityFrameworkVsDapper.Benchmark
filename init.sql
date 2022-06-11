
DROP DATABASE IF exists ef_dapper_benchmark;
CREATE DATABASE ef_dapper_benchmark;

USE ef_dapper_benchmark;

CREATE TABLE Benches (
    BenchId INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(255),
    Description VARCHAR(255),
    Cost INT,
    Height INT,
    Width INT,
    Depth INT,
    PRIMARY KEY (BenchId)
); 

CREATE INDEX IX_BENCHES_NAME
ON Benches (Name);

drop procedure if exists seedBenches;

CREATE PROCEDURE seedBenches()
BEGIN
    DECLARE i int DEFAULT 0;
    WHILE i <= 100000 DO
        INSERT INTO Benches 
        (
            Name, 
            Description, 
            Cost,
            Height,
            Width,
            Depth
        ) 
        VALUES 
        (
            (SELECT concat("Name ", i)),
            (SELECT concat("Description but with extra letters ", i)), 
            (SELECT FLOOR(RAND()*(1000-50+1)+50)),
            (SELECT FLOOR(RAND()*(10-5+1)+5)),
            (SELECT FLOOR(RAND()*(10-5+1)+5)),
            (SELECT FLOOR(RAND()*(10-5+1)+5))
        );
        SET i = i + 1;
    END WHILE;
END;

CALL seedBenches();
