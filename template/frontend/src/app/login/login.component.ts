import { Component, inject } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { InputOtpModule } from 'primeng/inputotp';
import { PasswordModule } from 'primeng/password';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { error } from 'console';

@Component({
  selector: 'app-login',
  imports: [InputTextModule, InputOtpModule, FormsModule, PasswordModule, FloatLabelModule, ButtonModule, ToastModule],
  providers: [MessageService],
  templateUrl: './login.component.html',
  styleUrl: './login.component.less'
})

export class LoginComponent {
  private messageService = inject(MessageService);
  private authService = inject(AuthService);
  email = '';
  password = ''

  login = async () =>  {
    await this.authService.login(this.email, this.password)
      .then((result) => {
        this.authService.saveUserToken(result.data.data.token);
      })
      .catch(() => {
        this.messageService.add({ severity: 'error', summary: 'Login Error', detail: 'Wrong email or password'})
      })
  }
}
