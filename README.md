# Project: alex-claude-contacts — VPS Setup Session
**Date:** April 5, 2026  
**Goal:** Set up a VPS in Russia to host a personal contacts web application (accessible from Mac, Windows, Android)

---

## Project Architecture

```
Your Mac (write code here)
        ↓ deploy via scp
   VPS Server in St. Petersburg
   ├── Nginx (serves frontend)
   └── FastAPI (backend, handles contacts)
        └── SQLite (database)
        ↑
Browser (Mac / Windows / Android)
```

**Stack:** FastAPI + SQLite + plain HTML/JS + Nginx on Ubuntu VPS

**Access:** Via IP address in browser (no domain needed for now)

**Security:** Password login page + brute-force protection (IP ban after N failed attempts)

---

## Step 1 — Choosing a VPS Provider

### Providers Tried and Rejected

- **4VPS.su** — cheapest (~80 RUB/month), servers in Moscow — **REJECTED: all servers SOLD OUT** (reason: Telegram blocking wave caused mass VPN server purchases)
- **SprintHost (sprinthost.ru)** — 140 RUB/month, servers in St. Petersburg — initially considered too expensive and wrong city

### Provider Chosen

**SprintBox by SprintHost**  
Website: [cp.sprintbox.ru](https://cp.sprintbox.ru)  
Phone: **8-800-555-78-23** (free, 24/7)  
Email: support@sprinthost.ru  
Location: St. Petersburg ✅ (decided it's fine — latency difference vs Moscow is zero for our use case)

**Tariff:** "Студент" (Student)
- CPU: 1 core
- RAM: 0.5 GB
- NVMe: 7 GB
- Price: **140 RUB/month**
- Paid: 500 RUB upfront (~3.5 months)

---

## Step 2 — Creating the Box

### Box Name
```
alex-claude-contacts-02
```
(alex-claude-contacts-01 was created with a custom ISO — see Errors section)

### Server Details
- IP address: `<ip address>`
- Location: Russia, St. Petersburg
- OS: Ubuntu 25.10 "Questing Quokka" (Server, no GUI)
- User: `root`

### Closed Ports (pre-configured by provider)
```
25, 389, 465, 587, 2525, 3389, 53413
```
These are not needed for our project.

---

## Step 3 — Connecting via SSH

### First Connection (from Mac Terminal)
Open Terminal on Mac: `Cmd + Space` → type "Terminal" → Enter

```bash
ssh root@<ip address>
```

First time you'll see:
```
The authenticity of host '<ip address>' can't be established.
ED25519 key fingerprint is: SHA256:...
Are you sure you want to continue connecting (yes/no/[fingerprint])? yes
```
Type `yes` and press Enter.

### Changing the Expired Password
On first login the server forces a password change:
```
WARNING: Your password has expired.
You must change your password now and log in again!
New password:
```
Enter your new password twice. After that the connection closes automatically. Reconnect:
```bash
ssh root@<ip address>
```

### Successful Login Output
```
Welcome to Ubuntu 25.10 (GNU/Linux 6.17.0-20-generic x86_64)
...
root@box-909266:~#
```
The prompt `root@box-909266:~#` means you are now controlling the server in St. Petersburg.

---

## Step 4 — Setting Up SSH Keys (No More Password Prompts)

Run this on your **Mac** (not on the server):
```bash
ssh-copy-id root@<ip address>
```
Enter the password one last time. After this, SSH and SCP work without any password.

---

## Step 5 — Server Updates

After connecting to the server, run:

```bash
apt update
```
Updates the list of available packages.

```bash
apt upgrade -y
```
Installs available updates. Output included:
```
Upgrading: 1, Installing: 0, Removing: 0, Not Upgrading: 0
1 standard LTS security update
...
No services need to be restarted.
```

---

## Step 6 — Testing File Transfer (scp)

### Create a test file on Mac
```bash
echo "привет от Алекса" > файл.py
```

### Copy it to the server
```bash
scp файл.py root@<ip address>:/root/
```
Output:
```
файл.py     100%   31     2.2KB/s   00:00
```

### Verify on the server
```bash
ls /root/
cat *
```
Output:
```
файл.py
привет от Алекса
```
✅ File successfully transferred from Mac to St. Petersburg server.

---

## Errors & Problems Encountered

### ❌ Error 1: Custom ISO — Kernel Panic
**What happened:** The first box (alex-claude-contacts-01) was created using a direct Ubuntu ISO link:
```
https://releases.ubuntu.com/22.04/ubuntu-22.04.5-live-server-amd64.iso
```
**Error shown in VNC console:**
```
Kernel panic - not syncing: No working init found.
Try passing init= option to kernel.
```
**Reason:** SprintBox cannot automatically install Ubuntu from a raw ISO image. The ISO is an installer, not a ready-to-run image.  
**Fix:** Delete the box, create a new one using SprintBox's built-in image list → "Линуксы" → Ubuntu.  
**Cost:** Lost ~5 RUB from the failed box.

### ❌ Error 2: "Reset root password" button was greyed out
**What happened:** On the first (broken) box, the "Сброс пароля root" button in the control panel was unavailable.  
**Reason:** The box was created from a custom ISO and never successfully booted, so the password reset system couldn't work.  
**Fix:** Reinstall the box using a standard image, or delete and create a new box.

### ❌ Error 3: No password in the welcome email
**What happened:** After creating the second box, the welcome email did not contain the root password.  
**Reason:** The box was created with the "Новый SSH-ключ" / password auth flow — the password was shown only during box creation in the control panel, not emailed separately.  
**Fix:** In the control panel → three dots menu (⋮) next to the box → "Сброс пароля root" → new password is emailed.

---

## Additional Questions Asked During Session

- **What is a VDS/VPS?** — A virtual slice of a real physical server. Like renting one room in a big building.
- **What is Ubuntu 22.04 / 25.10?** — Version number = release year and month. Like Windows 10/11.
- **What is NVMe?** — Fast type of storage disk. Evolution: HDD (spinning plates) → SSD (flash, no plates) → NVMe (flash + faster connection to CPU).
- **What is RAM?** — Memory that loses everything when power is off. Unlike NVMe which keeps data permanently.
- **Why is 0.5 GB RAM enough?** — Ubuntu Server has no graphical interface, so it uses very little RAM. Our simple app needs even less.
- **What is a "Promo" tariff?** — Discounted entry-level plan to attract new customers. Slightly fewer resources but fine for our project.
- **What does "4ms" mean?** — Ping latency (response time). 4 milliseconds = very fast, nearly instant.
- **Why Moscow vs St. Petersburg?** — For our use case (single user, simple app) there is zero practical difference. St. Petersburg was chosen because SprintBox had available servers and good support.
- **Does SprintHost only work in spring?** — No 😄 "Sprint" means a fast run, not the season.
- **What is Happ app?** — A VPN/proxy client app for Android. ESET antivirus flags it because it routes traffic through external servers — that's the app's intended function, not a virus. The app does not collect user data.
- **Why did we run apt update and apt upgrade?** — Like Windows Update. Patches security vulnerabilities before we start building.
- **What does "deploy" mean?** — Copying your code from your local machine to the server so it runs there 24/7.

---

## What's Next (Next Session)

1. Install Python3 + pip + venv on the server
2. Ask Claude Opus to write the FastAPI backend (contacts CRUD + brute-force protection)
3. Ask Claude Opus to write the frontend (HTML + CSS + JS, single file)
4. Deploy both to the server via `scp`
5. Install and configure Nginx
6. Test from Mac, Windows, Android browser

---

## Useful Commands Reference

| Command | What it does |
|---|---|
| `ssh root@<ip address>` | Connect to server |
| `ssh-copy-id root@<ip address>` | Copy SSH key (no more passwords) |
| `scp file.py root@<ip address>:/root/` | Copy file from Mac to server |
| `apt update` | Refresh package list |
| `apt upgrade -y` | Install updates |
| `ls` | List files in current directory |
| `pwd` | Show current directory path |
| `cat filename` | Show file contents |
| `Cmd + N` | New Terminal window on Mac |

---

## Provider Contact Info

| Provider | Status | Phone | Notes |
|---|---|---|---|
| 4VPS.su | ❌ Rejected | — | All servers sold out |
| SprintHost / SprintBox | ✅ Active | 8-800-555-78-23 | Our provider, free call |

---

*Session conducted with Claude Sonnet (claude.ai). Next session: use Claude Opus for code generation.*
