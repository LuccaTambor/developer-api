import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private BASE_URL = '/api/Auth';

  constructor() { }

  async login(email: string, password: string) {
    const request = { email, password };
    const result = await fetch(this.BASE_URL, {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      method: "POST",
      body: JSON.stringify(request)
    })
    return result.json();
  }

  saveUserToken(token : string) {
    window.localStorage.setItem('userToken', token)
  }
}
