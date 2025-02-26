import { Component } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { InputOtpModule } from 'primeng/inputotp';
import { PasswordModule } from 'primeng/password';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [InputTextModule, InputOtpModule, FormsModule, PasswordModule, FloatLabelModule, ButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.less'
})

export class LoginComponent {
  username = '';
  password = ''

  login = () =>  {
    console.log(`Your username is ${this.username}`);
  }
}
