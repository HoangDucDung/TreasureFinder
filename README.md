## Hướng dẫn tạo cơ sở dữ liệu TreasureHunt

### 1) Tạo database và các bảng

Chạy lần lượt script SQL dưới đây trong SQL Server (SSMS/Azure Data Studio):

```sql
CREATE DATABASE TreasureHunt;
GO
USE TreasureHunt;
GO

-- Tạo bảng MapConfiguration với GUID
CREATE TABLE MapConfiguration (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(), -- GUID tự sinh
    NumRows INT NOT NULL,       -- Số hàng n
    NumCols INT NOT NULL,       -- Số cột m
    NumChestTypes INT NOT NULL, -- Số loại rương p
    MinimumFuel FLOAT NULL,     -- Lượng nhiên liệu tối thiểu
    OptimalPath NVARCHAR(MAX) NULL -- Đường đi tối ưu
);

-- Tạo bảng Islands với khóa ngoại GUID
CREATE TABLE Islands (
    MapId UNIQUEIDENTIFIER NOT NULL,
    RowPosition INT NOT NULL,   -- Vị trí hàng của đảo
    ColPosition INT NOT NULL,   -- Vị trí cột của đảo
    ChestNumber INT NOT NULL,   -- Số thứ tự của rương (từ 1 đến p)
    PRIMARY KEY (MapId, RowPosition, ColPosition),
    FOREIGN KEY (MapId) REFERENCES MapConfiguration(Id)
);
```

### 2) Cấu hình chuỗi kết nối (nếu cần)

- Mở `appsettings.json` (hoặc `appsettings.Development.json`) của API.
- Thêm hoặc cập nhật `ConnectionStrings:DefaultConnection` trỏ tới SQL Server của bạn, ví dụ:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TreasureHunt;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Thay đổi `Server`, `Trusted_Connection`/`User Id`/`Password` theo môi trường thực tế.


