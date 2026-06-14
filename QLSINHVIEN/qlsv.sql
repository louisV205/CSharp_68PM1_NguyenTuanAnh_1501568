IF DB_ID(N'qlsv') IS NULL
BEGIN
    CREATE DATABASE [qlsv];
END
GO

USE [qlsv];
GO

IF OBJECT_ID(N'dbo.sinhviens', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.sinhviens;
END
GO

IF OBJECT_ID(N'dbo.lophocs', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.lophocs;
END
GO

CREATE TABLE dbo.lophocs
(
    id INT IDENTITY(1,1) NOT NULL,
    malop VARCHAR(20) NOT NULL,
    tenlop NVARCHAR(100) NOT NULL,
    ghichu NVARCHAR(255) NULL,

    CONSTRAINT PK_lophocs PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_lophocs_malop UNIQUE (malop)
);
GO

CREATE TABLE dbo.sinhviens
(
    id INT IDENTITY(1,1) NOT NULL,
    masv VARCHAR(20) NOT NULL,
    hoten NVARCHAR(100) NOT NULL,
    ngaysinh DATE NOT NULL,
    gioitinh NVARCHAR(10) NOT NULL,
    malop VARCHAR(20) NOT NULL,

    CONSTRAINT PK_sinhviens PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_sinhviens_masv UNIQUE (masv),
    CONSTRAINT FK_sinhviens_lophocs_malop
        FOREIGN KEY (malop) REFERENCES dbo.lophocs(malop)
        ON UPDATE CASCADE
        ON DELETE NO ACTION
);
GO

CREATE INDEX IX_sinhviens_malop ON dbo.sinhviens(malop);
CREATE INDEX IX_sinhviens_search ON dbo.sinhviens(masv, hoten, malop);
CREATE INDEX IX_lophocs_search ON dbo.lophocs(malop, tenlop);
GO

INSERT INTO dbo.lophocs (malop, tenlop, ghichu)
VALUES
    ('68PM1', N'Lop 68PM1', N'Lop cong nghe thong tin'),
    ('68PM2', N'Lop 68PM2', N'Lop cong nghe thong tin'),
    ('68PM3', N'Lop 68PM3', N'Lop cong nghe thong tin');
GO

INSERT INTO dbo.sinhviens (masv, hoten, ngaysinh, gioitinh, malop)
VALUES
    ('SV001', N'Nguyen Van A', '2004-01-15', N'nam', '68PM1'),
    ('SV002', N'Tran Thi B', '2004-05-20', N'nu', '68PM1'),
    ('SV003', N'Le Van C', '2003-09-10', N'nam', '68PM2');
GO
