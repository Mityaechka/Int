import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
import { SponsorBanner } from '../entities/sponsor-banner.entity';

@Injectable({
  providedIn: 'root',
})
export class BannersService {
  constructor(private http: HttpService) {}
  async getBanners() {
    return await this.http.get<SponsorBanner[]>(`banners`);
  }
  async getBanner(id: number) {
    return await this.http.get<SponsorBanner>(`banners/${id}`);
  }
  async createBanner(data: { url: string; name: string; file: any }) {
    return await this.http.postForm<SponsorBanner>(`banners/create`, data);
  }
  async editBanner(id: number, data: { url: string; name: string; file: any }) {
    return await this.http.postForm<SponsorBanner>(`banners/${id}/edit`, data);
  }

  async gameBanners(gameId: number) {
    return await this.http.get<SponsorBanner[]>(
      `banners/gameBanners?gameId=${gameId}`
    );
  }
  async addToGame(bannerId: number, gameId: number) {
    return await this.http.get<SponsorBanner[]>(
      `banners/addToGame?gameId=${gameId}&bannerId=${bannerId}`
    );
  }
}
