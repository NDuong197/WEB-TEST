const apiUrl = 'http://localhost:5190/api/Goods'; // Thay bằng port của Web API của bạn

// Lấy và hiển thị danh sách hàng hóa khi trang tải
window.onload = fetchGoods;

async function fetchGoods() {
    try {
        const response = await fetch(apiUrl);
        const goods = await response.json();
        const goodsList = document.getElementById('goods-list');
        goodsList.innerHTML = '';
        goods.forEach(good => {
            const li = document.createElement('li');
            li.textContent = `Mã: ${good.maHangHoa} - Tên: ${good.tenHangHoa} - Số lượng: ${good.soLuong} - Ghi chú: ${good.ghiChu}`;
            goodsList.appendChild(li);
        });
    } catch (error) {
        console.error('Lỗi khi lấy danh sách hàng hóa:', error);
    }
}

// Tạo hoặc cập nhật hàng hóa
async function createOrUpdateGood() {
    const maHangHoa = document.getElementById('ma_hanghoa').value.trim();
    const tenHangHoa = document.getElementById('ten_hanghoa').value.trim();
    const soLuong = document.getElementById('so_luong').value;
    const ghiChu = document.getElementById('ghi_chu').value.trim();

    const regex = /^[A-Za-z0-9]{9}$/;
    if (!regex.test(maHangHoa)) {
        alert('Mã hàng hóa phải có đúng 9 ký tự!');
        return;
    }

    const good = { maHangHoa, tenHangHoa, soLuong: parseInt(soLuong), ghiChu };

    let method = 'POST'; // Mặc định là thêm mới
    let url = apiUrl;

    // Kiểm tra nếu hàng hóa đã tồn tại thì dùng PUT để cập nhật
    try {
        const checkResponse = await fetch(`${apiUrl}/${maHangHoa}`);
        if (checkResponse.ok) {
            method = 'PUT'; // Nếu tìm thấy hàng hóa, chuyển thành cập nhật
            url = `${apiUrl}/${maHangHoa}`;
        }
    } catch (error) {
        console.error('Lỗi khi kiểm tra hàng hóa:', error);
    }

    try {
        const response = await fetch(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(good)
        });

        if (response.ok) {
            alert(method === 'PUT' ? 'Cập nhật thành công!' : 'Thêm thành công!');
            fetchGoods(); // Cập nhật lại danh sách
        } else {
            alert('Có lỗi xảy ra!');
        }
    } catch (error) {
        console.error('Lỗi:', error);
    }
}



// Xóa hàng hóa
async function deleteGood() {
    const maHangHoa = document.getElementById('ma_hanghoa').value;
    if (!maHangHoa) {
        alert('Vui lòng nhập mã hàng hóa để xóa!');
        return;
    }

    try {
        const response = await fetch(`${apiUrl}/${maHangHoa}`, {
            method: 'DELETE'
        });
        if (response.ok) {
            alert('Xóa thành công!');
            fetchGoods(); // Cập nhật lại danh sách
        } else {
            alert('Có lỗi xảy ra!');
        }
    } catch (error) {
        console.error('Lỗi:', error);
    }
}
