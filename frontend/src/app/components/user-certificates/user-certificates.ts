import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CertificateService, UserCertificate } from '../../services/certificate';

@Component({
  selector: 'app-user-certificates',
  imports: [CommonModule, FormsModule],
  templateUrl: './user-certificates.html',
  styleUrl: './user-certificates.scss'
})
export class UserCertificatesComponent implements OnInit {
  certificates: UserCertificate[] = [];
  total = 0;
  page = 1;
  pageSize = 5;
  totalPages = 1;

  showForm = false;
  newCert: UserCertificate = {
    commonName: '',
    email: '',
    rootCertificateId: '',
    csrData: '',
    pemData: '',
    expiresAt: ''
  };

  constructor(private certService: CertificateService) {}

  ngOnInit(): void {
    this.loadCertificates();
  }

  loadCertificates(): void {
    this.certService.getUserCertificates(this.page, this.pageSize).subscribe(result => {
      this.certificates = result.data;
      this.total = result.total;
      this.totalPages = Math.ceil(this.total / this.pageSize);
    });
  }

  createCertificate(): void {
    this.certService.createUserCertificate(this.newCert).subscribe(() => {
      this.loadCertificates();
      this.showForm = false;
      this.newCert = {
        commonName: '',
        email: '',
        rootCertificateId: '',
        csrData: '',
        pemData: '',
        expiresAt: ''
      };
    });
  }

  deleteCertificate(id: string): void {
    if (confirm('Biztosan törölni szeretnéd?')) {
      this.certService.deleteUserCertificate(id).subscribe(() => {
        this.loadCertificates();
      });
    }
  }

  prevPage(): void {
    if (this.page > 1) {
      this.page--;
      this.loadCertificates();
    }
  }

  nextPage(): void {
    if (this.page < this.totalPages) {
      this.page++;
      this.loadCertificates();
    }
  }
}