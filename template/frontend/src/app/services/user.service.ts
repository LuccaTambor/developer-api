import { Injectable } from '@angular/core';
import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})

export class UserService {
  private BASE_URL = '/api/Users';

  async create(user: User) {
    const result = await fetch(this.BASE_URL, {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      method: "POST",
      body: JSON.stringify(user)
    })
    return result.json();
  }
  constructor() { }
}
