tg://proxy?server=tgwh.magproxy.com&port=8443&secret=eeb30fc4df699024f7a5dec0b0e66524b16465762e6373373737372e766b2e7275


# udm

# Установка SOCKS5-сервера на Ubuntu Server в VirtualBox

> Инструкция по настройке SOCKS5-прокси сервера на базе Ubuntu Server в VirtualBox с подключением из Windows 10.

---

## Содержание

1. [Установка VirtualBox](#1-установка-virtualbox)
2. [Установка Ubuntu Server](#2-установка-ubuntu-server)
3. [Первый вход и настройка SSH](#3-первый-вход-и-настройка-ssh)
4. [Настройка проброса портов в VirtualBox](#4-настройка-проброса-портов-в-virtualbox)
5. [Подключение по SSH из Windows](#5-подключение-по-ssh-из-windows)
6. [Установка и настройка Dante SOCKS5](#6-установка-и-настройка-dante-socks5)
7. [Проверка работы SOCKS5](#7-проверка-работы-socks5)
8. [Как это работает](#8-как-это-работает)

---

## 1. Установка VirtualBox

### 1.1 Microsoft Visual C++ 2019 Redistributable

VirtualBox 7.2.6 требует наличия **Microsoft Visual C++ 2019 Redistributable**.

Скачай и установи:
- https://aka.ms/vs/17/release/vc_redist.x64.exe

### 1.2 VirtualBox

Скачай установщик для Windows с официального сайта:
- https://www.virtualbox.org/wiki/Downloads → **Windows hosts**

Также скачай **VirtualBox Extension Pack** на той же странице и установи его через:
`Файл → Инструменты → Extension Pack Manager`

---

## 2. Установка Ubuntu Server

### 2.1 Скачать образ

Скачай Ubuntu Server LTS:
- https://ubuntu.com/download/server → **Download Ubuntu Server 24.04 LTS** (~2 ГБ)

### 2.2 Создать виртуальную машину

В VirtualBox создай новую VM и укажи скачанный `.iso` файл как загрузочный диск.

Рекомендуемые параметры:
- RAM: минимум 1 ГБ (лучше 2 ГБ)
- Диск: минимум 10 ГБ
- Сеть: NAT (по умолчанию)

### 2.3 Установка Ubuntu Server

В процессе установки обрати внимание на:

| Шаг | Что делать |
|-----|-----------|
| Язык / раскладка | English (стандарт для серверов) |
| Hostname | Любое имя, например `ubuntu-server` |
| Имя пользователя | Запомни! Нужен для входа |
| Пароль | Запомни! Нужен для входа и sudo |
| **OpenSSH server** | ✅ Обязательно поставить галочку |

> Если галочку на OpenSSH не поставил — не страшно, SSH можно установить вручную после загрузки системы (см. п. 3.2).

---

## 3. Первый вход и настройка SSH

### 3.1 Первый вход

После перезагрузки введи имя пользователя и пароль.

> Пароль при вводе не отображается — это нормально.

После успешного входа увидишь строку:
```
username@hostname:~$
```

### 3.2 Установка OpenSSH (если не поставил при установке)

```bash
sudo apt update && sudo apt install openssh-server -y
```

### 3.3 Узнать IP-адрес Ubuntu

```bash
ip a
```

В выводе найди строку с `inet` — нужен адрес вида `10.0.x.x` или `192.168.x.x`.

В режиме NAT VirtualBox стандартный адрес: **`10.0.2.15`**

### 3.4 Узнать имя сетевого интерфейса

В выводе команды `ip a` найди строку с `inet 10.0.2.15` — **над ней** будет имя интерфейса.

Стандартное имя в VirtualBox: **`enp0s3`**

---

## 4. Настройка проброса портов в VirtualBox

Так как VM работает в режиме **NAT**, Windows не может напрямую подключиться к Ubuntu. Нужно настроить проброс портов.

**VM можно не выключать.**

Перейди в: `Настройки VM → Сеть → Адаптер 1 → Дополнительно → Проброс портов`

Добавь два правила:

| Имя | Протокол | Хост IP | Хост порт | Гость IP | Гость порт |
|-----|----------|---------|-----------|----------|------------|
| ssh | TCP | 127.0.0.1 | 2222 | 10.0.2.15 | 22 |
| socks5 | TCP | 127.0.0.1 | 1080 | 10.0.2.15 | 1080 |

Нажми **ОК**.

---

## 5. Подключение по SSH из Windows

Открой **PowerShell** или **Командную строку** на Windows и выполни:

```powershell
ssh -p 2222 username@127.0.0.1
```

> Замени `username` на своё имя пользователя Ubuntu.

При первом подключении система спросит подтвердить fingerprint — введи `yes`.

После этого все команды можно вводить в окне Windows с поддержкой копипаста.

---

## 6. Установка и настройка Dante SOCKS5

### 6.1 Обновить систему

```bash
sudo apt update && sudo apt upgrade -y
```

### 6.2 Установить Dante

```bash
sudo apt install dante-server -y
```

### 6.3 Настроить конфиг

Запиши конфигурацию одной командой:

```bash
sudo tee /etc/danted.conf << 'EOF'
logoutput: syslog

internal: 0.0.0.0 port = 1080
external: enp0s3

clientmethod: none
socksmethod: none

user.privileged: root
user.notprivileged: nobody

client pass {
    from: 0.0.0.0/0 to: 0.0.0.0/0
    log: connect disconnect
}

socks pass {
    from: 0.0.0.0/0 to: 0.0.0.0/0
    log: connect disconnect
}
EOF
```

> Если имя интерфейса отличается от `enp0s3` — замени его на своё в строке `external:`.

### 6.4 Запустить Dante

```bash
sudo systemctl restart danted
```

### 6.5 Проверить статус

```bash
sudo systemctl status danted
```

Должно быть:
```
Active: active (running)
```

### 6.6 Включить автозапуск при старте системы

```bash
sudo systemctl enable danted
```

---

## 7. Проверка работы SOCKS5

На Windows в PowerShell выполни:

```powershell
curl.exe --proxy socks5://127.0.0.1:1080 https://api.ipify.org
```

> Обязательно `curl.exe`, а не `curl` — в PowerShell `curl` это псевдоним для другой команды.

Если всё работает — получишь в ответ IP-адрес (это IP твоего интернет-провайдера, через который выходит Ubuntu).

---

## 8. Как это работает

```
Windows (curl.exe)
        ↓
127.0.0.1:1080
        ↓
Проброс портов VirtualBox
        ↓
Ubuntu 10.0.2.15:1080 — Dante SOCKS5 сервер
        ↓
Интернет
        ↓
Ответ возвращается обратно по той же цепочке
```

**SOCKS5** — это протокол прокси, который позволяет направлять любой TCP/UDP трафик через сервер-посредник. В отличие от HTTP-прокси, SOCKS5 работает на более низком уровне и поддерживает любые приложения и протоколы.

---

## Полезные команды

```bash
# Статус Dante
sudo systemctl status danted

# Перезапустить Dante
sudo systemctl restart danted

# Остановить Dante
sudo systemctl stop danted

# Посмотреть логи Dante
sudo journalctl -u danted -f

# Посмотреть сетевой интерфейс и IP
ip a
```

---

*Инструкция составлена: март 2026*
