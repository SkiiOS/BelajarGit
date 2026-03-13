# Panduan Penggunaan Git & Repositori

Selamat datang di repositori proyek! Dokumen ini berisi standar operasional dan panduan menggunakan Git agar kolaborasi tim berjalan lancar, rapi, dan meminimalisir *conflict*, terutama saat bekerja dengan file berukuran besar (*assets*).

---

## 1. Inisialisasi Awal & Git LFS (Large File Storage)

Karena proyek ini menggunakan banyak aset berukuran besar (seperti 3D models, audio, tekstur), kita **wajib** menggunakan Git LFS.

**Langkah Pertama (Hanya dilakukan sekali saat baru bergabung):**

1. Pastikan Git dan [Git LFS](https://git-lfs.github.com/) sudah terinstal di komputermu.
2. *Clone* repositori ke komputermu:
   ```bash
   git clone <link-repositori>
3. masuk ke folder proyek
   ```bash
   cd nama-folder-proyek
4. masuk ke folder proyek
   ```bash
   git lfs install

## 2. Alur Kerja Harian (Daily Workflow)

Untuk mencegah bentrok (merge conflict) dengan pekerjaan anggota tim lain, biasakan melakukan urutan ini setiap kali kamu mulai dan selesai bekerja.

1. Sebelum Mulai Bekerja (Wajib!)
   ```bash
      # 1. Pastikan kamu berada di branch yang benar
      git checkout main  # atau develop, tergantung struktur tim
    
      # 2. Ambil informasi perubahan terbaru dari server
      git fetch origin
    
      # 3. Tarik perubahan terbaru ke komputermu
      git pull origin main

2. Setelah Selesai Bekerja / Ingin Menyimpan Progress
   ```bash
     #1. Cek apa saja file yang berubah
     git status

     #2. Tambahkan file yang ingin disimpan (gunakan `git add .` untuk semua file)
     git add .

     #3. Buat commit dengan pesan yang jelas
     git commit -m "feat: menambahkan script pergerakan karakter utama"

     #4. Upload perubahan ke server
     git push origin nama-branch-kamu
   
