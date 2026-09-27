# 🎮 Caro Online

Ứng dụng chơi **Caro Online** được xây dựng bằng **WPF**, **ASP.NET Core** và **SignalR**, cho phép các client kết nối và chơi Caro theo thời gian thực.

## 🛠️ Công nghệ

* C# / .NET
* WPF
* ASP.NET Core
* SignalR

## 🏗️ Cấu trúc

```text
CaroOnlineSolution
│
├── Caro.WPF       # Client WPF
├── Server         # ASP.NET Core Server
└── CaroOnlineSolution.slnx
```

## ⚡ Hoạt động

```text
WPF Client
    │
    │ SignalR
    ▼
ASP.NET Core Server
    │
    │ SignalR
    ▼
WPF Client
```

Server chịu trách nhiệm xử lý kết nối và đồng bộ trạng thái trò chơi giữa các client thông qua **SignalR**.

## 🚀 Chạy project

### 1. Clone repository

```bash
git clone https://github.com/huyhoanggw/CaroOnlineSolution.git
cd CaroOnlineSolution
```

### 2. Mở solution

Mở file:

```text
CaroOnlineSolution.slnx
```

bằng Visual Studio.

### 3. Chạy

Khởi động **Server** trước, sau đó chạy **Caro.WPF** để kết nối tới server.

## 🎯 Mục tiêu

Project được xây dựng nhằm thực hành:

* WPF
* ASP.NET Core
* SignalR
* Client – Server Architecture
* Real-time communication
