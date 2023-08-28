INSERT INTO Exps (Preview_Url, Preview, Url, Video)
SELECT 'C:/Users/1/Downloads/robin.png', BLOB1.BulkColumn, 'C:/Users/1/Downloads/robin.mp4', BLOB2.BulkColumn
FROM OPENROWSET(Bulk 'C:/Users/1/Downloads/robin.mp4', SINGLE_BLOB) AS BLOB1,
     OPENROWSET(Bulk 'C:/Users/1/Downloads/robin.png', SINGLE_BLOB) AS BLOB2

INSERT INTO Exps (Preview_Url, Preview, Url, Video)
SELECT 'C:/Users/1/Downloads/deku.png', BLOB1.BulkColumn, 'C:/Users/1/Downloads/deku.mp4', BLOB2.BulkColumn
FROM OPENROWSET(Bulk 'C:/Users/1/Downloads/deku.mp4', SINGLE_BLOB) AS BLOB1,
OPENROWSET(Bulk 'C:/Users/1/Downloads/deku.png', SINGLE_BLOB) AS BLOB2

INSERT INTO Exps (Preview_Url, Preview, Url, Video)
SELECT 'C:/Users/1/Downloads/luffytopjoyboy.png', BLOB1.BulkColumn, 'C:/Users/1/Downloads/luffytopjoyboy.mp4', BLOB2.BulkColumn
FROM OPENROWSET(Bulk 'C:/Users/1/Downloads/luffytopjoyboy.mp4', SINGLE_BLOB) AS BLOB1,
OPENROWSET(Bulk 'C:/Users/1/Downloads/luffytopjoyboy.png', SINGLE_BLOB) AS BLOB2