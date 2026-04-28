import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CertificateService, RootCertificate } from '../../services/certificate';

@Component({
  selector: 'app-root-certificates',
  imports: [CommonModule, FormsModule],
  templateUrl: './root-certificates.html',
  styleUrl: './root-certificates.scss'
})
export class RootCertificatesComponent implements OnInit {
  certificates: RootCertificate[] = [];
  total = 0;
  page = 1;
  pageSize = 5;
  totalPages = 1;

  showForm = false;
  newCert: RootCertificate = {
    commonName: '',
    organization: '',
    country: '',
    expiresAt: '',
    pemData: ''
  };

  constructor(private certService: CertificateService) {}

  ngOnInit(): void {
    this.loadCertificates();
  }

  loadCertificates(): void {
    this.certService.getRootCertificates(this.page, this.pageSize).subscribe(result => {
      this.certificates = result.data;
      this.total = result.total;
      this.totalPages = Math.ceil(this.total / this.pageSize);
    });
  }

  createCertificate(): void {
    this.certService.createRootCertificate(this.newCert).subscribe(() => {
      this.loadCertificates();
      this.showForm = false;
      this.newCert = { commonName: '', organization: '', country: '', expiresAt: '', pemData: '' };
    });
  }

  deleteCertificate(id: string): void {
    if (confirm('Biztosan törölni szeretnéd?')) {
      this.certService.deleteRootCertificate(id).subscribe(() => {
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