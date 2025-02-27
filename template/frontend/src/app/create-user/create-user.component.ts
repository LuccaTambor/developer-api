import { Component, inject } from '@angular/core';
import { ToastModule } from 'primeng/toast';
import { InputTextModule } from 'primeng/inputtext';
import { FloatLabelModule } from 'primeng/floatlabel';
import { MessageService } from 'primeng/api';
import { FormsModule } from '@angular/forms';
import { InputMaskModule } from 'primeng/inputmask';
import { SelectModule } from 'primeng/select';
import { PasswordModule } from 'primeng/password';
import { ButtonModule } from 'primeng/button';
import { RouterLink, Router } from '@angular/router';
import { User } from '../models/User';
import { UserService } from '../services/user.service';

interface Role {
  name: string;
  code: number;
}

@Component({
  selector: 'app-create-user',
  imports: [ ToastModule, InputTextModule, FloatLabelModule, FormsModule, InputMaskModule, SelectModule, PasswordModule, ButtonModule, RouterLink ],
  providers: [MessageService ],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.less'
})

export class CreateUserComponent {
  private messageService = inject(MessageService);
  private userService = inject(UserService);
  private router = inject(Router);

  roles: Role[]  = [
    { name: 'Customer', code: 1},
    { name: 'Manager', code: 2},
    { name: 'Admin', code: 3},
  ];

  confirmPassword: string=  '';
  submitted = false;

  newUser : User= {
    username: '',
    email: '',
    password: '',
    phone: '',
    role: 0,
    status: 1
  };


  onSubmit = async () => {
    console.log(this.newUser)
    if(this.newUser.password != this.confirmPassword) {
      this.messageService.add({ severity: 'error', summary: 'Passwords not matching', detail: 'Both passwords should be equal'});
      return;
    }
    
    await this.userService.create(this.newUser)
      .then((result) => {
        this.messageService.add({ severity: 'success', summary: 'User Creation', detail: 'User created with success!'});
        this.router.navigateByUrl('login');       
      })
      .catch(() => {
        this.messageService.add({ severity: 'error', summary: 'Creation Error', detail: 'Error when trying to create user'});
      })
  }
}
