-- Bảng NguoiDung: Quản lý tài khoản đăng nhập
CREATE TABLE NguoiDung (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(255) NOT NULL, -- Nên mã hóa mật khẩu (SHA256/BCrypt)
    HoTen NVARCHAR(100) NOT NULL,
    VaiTro NVARCHAR(50), -- 'Admin', 'Thủ thư'
    TrangThai BIT DEFAULT 1 -- 1: Hoạt động, 0: Khóa
);
GO

-- Bảng TheLoai: Danh mục thể loại sách
CREATE TABLE TheLoai (
    MaTheLoai INT PRIMARY KEY IDENTITY(1,1),
    TenTheLoai NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- Bảng TacGia: Danh mục tác giả
CREATE TABLE TacGia (
    MaTacGia INT PRIMARY KEY IDENTITY(1,1),
    TenTacGia NVARCHAR(100) NOT NULL,
    GhiChu NVARCHAR(255)
);
GO

-- Bảng NhaXuatBan: Danh mục nhà xuất bản
CREATE TABLE NhaXuatBan (
    MaNXB INT PRIMARY KEY IDENTITY(1,1),
    TenNXB NVARCHAR(150) NOT NULL,
    DiaChi NVARCHAR(255),
    SoDienThoai VARCHAR(20)
);
GO

-- Bảng Sach: Thông tin chi tiết về sách
CREATE TABLE Sach (
    MaSach VARCHAR(10) PRIMARY KEY,
    TenSach NVARCHAR(200) NOT NULL,
    MaTheLoai INT,
    MaTacGia INT,
    MaNXB INT,
    NamXuatBan INT,
    SoLuong INT NOT NULL DEFAULT 0,
    MoTa NVARCHAR(MAX)
);
GO

-- Bảng DocGia: Quản lý thông tin độc giả
CREATE TABLE DocGia (
    MaDocGia VARCHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    Email VARCHAR(100) UNIQUE,
    SoDienThoai VARCHAR(20),
    DiaChi NVARCHAR(255)
);
GO

-- Bảng PhieuMuon: Quản lý thông tin chung của một lần mượn
CREATE TABLE PhieuMuon (
    MaPhieuMuon INT PRIMARY KEY IDENTITY(1,1),
    MaDocGia VARCHAR(10) NOT NULL,
    TenDangNhap VARCHAR(50), -- Thủ thư tạo phiếu
    NgayMuon DATE NOT NULL DEFAULT GETDATE(),
    HanTra DATE NOT NULL,
    TrangThai NVARCHAR(50) -- 'Đang mượn', 'Đã trả', 'Quá hạn'
);
GO

-- Bảng ChiTietPhieuMuon: Quản lý các sách được mượn trong một phiếu
CREATE TABLE ChiTietPhieuMuon (
    MaPhieuMuon INT NOT NULL,
    MaSach VARCHAR(10) NOT NULL,
    NgayTraThucTe DATE NULL,
    TienPhat DECIMAL(18, 0) DEFAULT 0,
    GhiChu NVARCHAR(255),
    PRIMARY KEY (MaPhieuMuon, MaSach)
);
GO

-- =================================================================
-- TẠO RÀNG BUỘC KHÓA NGOẠI (FOREIGN KEY)
-- =================================================================

ALTER TABLE Sach
ADD CONSTRAINT FK_Sach_TheLoai FOREIGN KEY (MaTheLoai) REFERENCES TheLoai(MaTheLoai),
    CONSTRAINT FK_Sach_TacGia FOREIGN KEY (MaTacGia) REFERENCES TacGia(MaTacGia),
    CONSTRAINT FK_Sach_NXB FOREIGN KEY (MaNXB) REFERENCES NhaXuatBan(MaNXB);
GO

ALTER TABLE PhieuMuon
ADD CONSTRAINT FK_PhieuMuon_DocGia FOREIGN KEY (MaDocGia) REFERENCES DocGia(MaDocGia),
    CONSTRAINT FK_PhieuMuon_NguoiDung FOREIGN KEY (TenDangNhap) REFERENCES NguoiDung(TenDangNhap);
GO

ALTER TABLE ChiTietPhieuMuon
ADD CONSTRAINT FK_CTPM_PhieuMuon FOREIGN KEY (MaPhieuMuon) REFERENCES PhieuMuon(MaPhieuMuon),
    CONSTRAINT FK_CTPM_Sach FOREIGN KEY (MaSach) REFERENCES Sach(MaSach);
GO

-- =================================================================
-- THÊM RÀNG BUỘC CHECK CHO TÍNH TOÀN VẸN DỮ LIỆU
-- =================================================================

ALTER TABLE NguoiDung
ADD CONSTRAINT CK_NguoiDung_VaiTro CHECK (VaiTro IN (N'Admin', N'Thủ thư'));

ALTER TABLE DocGia
ADD CONSTRAINT CK_DocGia_GioiTinh CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác'));

ALTER TABLE PhieuMuon
ADD CONSTRAINT CK_PhieuMuon_TrangThai CHECK (TrangThai IN (N'Đang mượn', N'Đã trả', N'Quá hạn'));

ALTER TABLE Sach
ADD CONSTRAINT CK_Sach_SoLuong CHECK (SoLuong >= 0),
    CONSTRAINT CK_Sach_NamXuatBan CHECK (NamXuatBan BETWEEN 1900 AND YEAR(GETDATE()));
GO

-- Tắt ràng buộc khóa ngoại tạm thời để xóa dữ liệu cũ (nếu có) và nạp lại
-- EXEC sp_MSforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
-- GO

-- Xóa dữ liệu cũ trong các bảng (chú ý thứ tự xóa ngược với thứ tự tạo)
-- DELETE FROM ChiTietPhieuMuon;
-- DELETE FROM PhieuMuon;
-- DELETE FROM Sach;
-- DELETE FROM DocGia;
-- DELETE FROM NguoiDung;
-- DELETE FROM TheLoai;
-- DELETE FROM TacGia;
-- DELETE FROM NhaXuatBan;
-- GO

-- =================================================================
-- PHẦN 1: THÊM DỮ LIỆU VÀO CÁC BẢNG DANH MỤC (BẢNG CHA)
-- =================================================================

-- Dữ liệu cho bảng TheLoai
INSERT INTO TheLoai (TenTheLoai) VALUES
(N'Văn học thiếu nhi'),
(N'Tiểu thuyết'),
(N'Khoa học viễn tưởng'),
(N'Lịch sử'),
(N'Trinh thám');
GO

-- Dữ liệu cho bảng TacGia
INSERT INTO TacGia (TenTacGia, GhiChu) VALUES
(N'Nguyễn Nhật Ánh', N'Nhà văn chuyên viết cho tuổi học trò'),
(N'Tô Hoài', N'Tác giả Dế Mèn phiêu lưu ký'),
(N'J.K. Rowling', N'Tác giả bộ truyện Harry Potter'),
(N'Agatha Christie', N'Nữ hoàng truyện trinh thám');
GO

-- Dữ liệu cho bảng NhaXuatBan
INSERT INTO NhaXuatBan (TenNXB, DiaChi, SoDienThoai) VALUES
(N'Nhà xuất bản Trẻ', N'161B Lý Chính Thắng, Quận 3, TP.HCM', '02839316289'),
(N'Nhà xuất bản Kim Đồng', N'55 Quang Trung, Hai Bà Trưng, Hà Nội', '02439434730'),
(N'Bloomsbury Publishing', N'London, UK', NULL);
GO

-- Dữ liệu cho bảng NguoiDung
-- Mật khẩu nên được mã hóa (hashed) trong ứng dụng thực tế
INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, VaiTro, TrangThai) VALUES
('admin', '123456', N'Quản trị viên', N'Admin', 1),
('thuthu01', '123456', N'Nguyễn Văn An', N'Thủ thư', 1);
GO

-- Dữ liệu cho bảng DocGia
INSERT INTO DocGia (MaDocGia, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, DiaChi) VALUES
('DG001', N'Trần Thị Bích', '2002-05-15', N'Nữ', 'bich.tran@email.com', '0901234567', N'123 Đường ABC, Quận 1, TP.HCM'),
('DG002', N'Lê Minh Cường', '2001-11-20', N'Nam', 'cuong.le@email.com', '0987654321', N'456 Đường XYZ, TP. Thủ Đức, TP.HCM');
GO


-- =================================================================
-- PHẦN 2: THÊM DỮ LIỆU VÀO CÁC BẢNG CÓ KHÓA NGOẠI (BẢNG CON)
-- =================================================================

-- Dữ liệu cho bảng Sach (sử dụng ID từ các bảng TheLoai, TacGia, NhaXuatBan)
INSERT INTO Sach (MaSach, TenSach, MaTheLoai, MaTacGia, MaNXB, NamXuatBan, SoLuong, MoTa) VALUES
('S001', N'Cho tôi xin một vé đi tuổi thơ', 1, 1, 1, 2008, 10, N'Tác phẩm nổi tiếng của Nguyễn Nhật Ánh.'),
('S002', N'Dế Mèn phiêu lưu ký', 1, 2, 2, 1941, 15, N'Tác phẩm kinh điển của Tô Hoài.'),
('S003', N'Harry Potter và Hòn đá Phù thủy', 3, 3, 3, 1997, 5, N'Tập đầu tiên của series Harry Potter.'),
('S004', N'Án mạng trên sông Nile', 5, 4, 1, 1937, 8, N'Vụ án của thám tử Hercule Poirot.');
GO


-- =================================================================
-- PHẦN 3: TẠO DỮ LIỆU GIAO DỊCH MƯỢN TRẢ
-- =================================================================

-- Dữ liệu cho bảng PhieuMuon
-- Giả sử hôm nay là 2025-10-17
INSERT INTO PhieuMuon (MaDocGia, TenDangNhap, NgayMuon, HanTra, TrangThai) VALUES
('DG001', 'thuthu01', '2025-10-10', '2025-10-24', N'Đang mượn'), -- Độc giả DG001 đang mượn
('DG002', 'thuthu01', '2025-09-01', '2025-09-15', N'Đã trả');    -- Độc giả DG002 đã trả
GO

-- Dữ liệu cho bảng ChiTietPhieuMuon
-- Chi tiết cho phiếu mượn 1 (MaPhieuMuon = 1)
INSERT INTO ChiTietPhieuMuon (MaPhieuMuon, MaSach, NgayTraThucTe, TienPhat, GhiChu) VALUES
(1, 'S001', NULL, 0, N'Sách còn mới'), -- Sách 'Cho tôi xin một vé đi tuổi thơ' chưa trả
(1, 'S003', NULL, 0, N'Sách còn mới'); -- Sách 'Harry Potter' chưa trả

-- Chi tiết cho phiếu mượn 2 (MaPhieuMuon = 2)
INSERT INTO ChiTietPhieuMuon (MaPhieuMuon, MaSach, NgayTraThucTe, TienPhat, GhiChu) VALUES
(2, 'S002', '2025-09-14', 0, N'Đã trả, tình trạng tốt'); -- Sách 'Dế mèn' đã trả
GO

-- Bật lại toàn bộ ràng buộc khóa ngoại
-- EXEC sp_MSforeachtable "ALTER TABLE ? WITH CHECK CHECK CONSTRAINT all"
-- GO

PRINT 'Thêm dữ liệu mẫu thành công!';


CREATE TABLE NhanVien (
    -- MaNV vừa là khóa chính, vừa là khóa ngoại liên kết tới NguoiDung.TenDangNhap
    MaNV VARCHAR(50) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(255),
    SoDienThoai VARCHAR(20),

    -- Thiết lập mối quan hệ một-một với bảng NguoiDung
    -- Nếu một NguoiDung bị xóa, NhanVien tương ứng cũng sẽ bị xóa
    CONSTRAINT FK_NhanVien_NguoiDung FOREIGN KEY (MaNV) REFERENCES NguoiDung(TenDangNhap) ON DELETE CASCADE
);

INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai)
VALUES
(
    'admin',                         -- MaNV phải khớp với TenDangNhap trong bảng NguoiDung
    N'Quản trị viên Hệ thống',       -- HoTen
    '1990-01-01',                    -- NgaySinh
    N'Nam',                          -- GioiTinh
    N'123 Đường ABC, TP.HCM',        -- DiaChi
    '0901234567'                     -- SoDienThoai
),
(
    'thuthu01',                      -- MaNV phải khớp với TenDangNhap trong bảng NguoiDung
    N'Nguyễn Văn An',                -- HoTen
    '1995-05-20',                    -- NgaySinh
    N'Nam',                          -- GioiTinh
    N'456 Đường XYZ, TP. Dĩ An',     -- DiaChi
    '0987654321'                     -- SoDienThoai
);

-- Cập nhật tất cả các vai trò không hợp lệ thành 'ThuThu'
UPDATE NguoiDung
SET VaiTro = 'ThuThu'
WHERE VaiTro NOT IN ('Admin', 'ThuThu', 'NhanVien') OR VaiTro IS NULL;

ALTER TABLE NguoiDung 
ADD CONSTRAINT CK_NguoiDung_VaiTro CHECK (VaiTro IN ('Admin', 'ThuThu', 'NhanVien'));
GO

select * from NguoiDung