import { Component, OnInit } from '@angular/core';
import { RegUser } from 'src/app/osoba';
import { NgForm } from '@angular/forms';
import { AuthHttpService } from 'src/app/services/auth.service';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FormArray } from '@angular/forms';
import { Router } from '@angular/router';

export class ImageSnippet {
  constructor(public src: string, public file: File) {}
}

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {
  
  registacijaForm = this.fb.group({
    name: ['', Validators.required],
    surname: ['', Validators.required],
    username: ['', Validators.required],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    email: ['', Validators.required],
    date: ['', Validators.required],
    ImageUrl: ['']
  });

  constructor(private http: AuthHttpService, private fb: FormBuilder, private router: Router) { }

  ngOnInit() {
  }



  selectedFile: ImageSnippet;


 
    processFile(imageInput: any) {
      const file: File = imageInput.files[0];
      const reader = new FileReader();
      let regModel: RegUser = this.registacijaForm.value;
    

      reader.addEventListener('load', (event: any) => {
        
        this.selectedFile = new ImageSnippet(event.target.result, file);
        const formData = new FormData();

        formData.append('image', this.selectedFile.file);
        regModel.File=formData;
        this.http.reg(regModel).subscribe(
          (res) => {
            console.log("ok");
          },
          (err) => {
             console.log(err)
          })
      });
  }

}
