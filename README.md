# Card Submit Go

## Folder :
### Asset
Untuk menyimpan sprite

### Script
Untuk menyimpan seluruh scripts

## Folder Script :

### Card
Terdapat 2 script utama, pertama yang ngurus mekanik kartu dan yang kedua yang mengurus ui kartu di mejanya. <br>
<img width="428" height="361" alt="image" src="https://github.com/user-attachments/assets/703ea1c1-d33d-4b86-b6f7-c628ea15154c" /><br>
Card manager bertugas dalam mengatur hierarchy dari sebuah jenis (yang 4 tipe) kartu, mengatur setiap jenis kartunya lambangnya apa, setiap lambang berapa banyak kartu, dan hasil kartu acaknya apa.<br>
<img width="431" height="147" alt="image" src="https://github.com/user-attachments/assets/6febcaa4-8252-4546-85c1-a9e1ce31dc90" /><br>
Card UI Prefab, berguna untuk menerima data kartu yang akan ditampilkan.<br>

### Manager - SceneController
Untuk mengatur perpindahan Scene (untuk kasus ini, cuman exit game)

### Manager - UIManager
Untuk mengatur mati nyala dari sebuah panel.

### Manager - GameManager
Untuk mengatur dari seluruh state yang ada. Untuk ganti state, ngecek state.

### Manager - MainMenuManager
Untuk mengatur main menu.

### Manager - GameOverManager
Untuk mengatur tampilan saat game selesai.

### Manager - GamePlayManager
Untuk mengatur flow dari gamenya. Seperti alur dari awal hingga akhir (lewwat event), nyawa player, score player

### Manager - Gameplay - UIGamePlayManager
Untuk mengatur UI dari gamenya. Seperti tampilan score, nyawa, kartu, game panel.

## Game FLow :
1. GameState di Game Manager menuju ke Main Menu dimana Main Menu Manager akan setup tampilannya
2. Kalau StartGame, akan ganti ke gameplay state dimana akan mengtrigger event gameStart (Buat deck dan acak deck, buka panel gameplay) dan reset semua kartu, score, nyawa, dll.
3. Setelah selesai, bakal trigger event Round Start (musuh akan ambil kartu, dan simpen di meja)
4. Kemudian, Player disuruh nebak lewat ui yang ditrigger lewat event Player Guess.
5. Kalau udah milih, bakal nampilin hasilnya dan trigger event conclusion (untuk ngecek hasilnya gimana, menang atau kalah) dan matiin ui milih tebakannya.
6. Nah kalau salah bakal kena damage dan cek apakah udah lebih kecil atau sama dengan 0 belum. kalau udah bakal ganti state ke gameover.
7. Selesai round juga akan trigger event Round Finish (buat trigger round start), dimana akan dijalankan dalamnya kalau nyawa player masih > 0.
8. Pas ambil kartu juga di cek, kalau udah abis, berarti ganti ke gameover.
